using AD.Services.ProtectedTime;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Game.Guild
{
    public class RecruitingModule : IRecruitingModule
    {
        private readonly GuildConfig guildConfig;
        private readonly RecruitingParams data;
        private readonly InventoryConfig inventoryConfig;

        private readonly GuildState guildState;
        private readonly RecruitingState recruitingState;

        private readonly IInventoryService inventoryService;
        private readonly ITimeService time;

        public IReadOnlyReactiveProperty<bool> IsEnabled => recruitingState.IsEnabled;

        public IJoinRequestsCollection Requests => recruitingState.Requests;
        public IReadOnlyList<ClassRoleSelectorInfo> ClassRoleSelectors => recruitingState.ClassRoleSelectors;

        public RecruitingModule(
            GuildState guildState,
            GuildConfig guildConfig,
            InventoryConfig inventoryConfig,
            IInventoryService inventoryService,
            ITimeService time,
            IObjectResolver resolver)
        {
            this.guildConfig = guildConfig;
            this.guildState = guildState;
            this.inventoryConfig = inventoryConfig;
            this.inventoryService = inventoryService;
            this.time = time;

            data = guildConfig.RecruitingParams;
            recruitingState = new(guildConfig, inventoryService, resolver);
        }

        public void SwitchRecruitingState()
        {
            recruitingState.SwitchRecruitingState();
        }

        public void Init()
        {
            recruitingState.Init();

            CreateDefaultRequests();
            UpdateOffileRequests();

            time.OnTick.Subscribe(OnUpdate);
        }

        public int AcceptJoinRequest(string requestId, out JoinRequestInfo info)
        {
            info = Requests.FirstOrDefault(x => x.Id == requestId);

            return RemoveRequest(requestId);
        }

        public int RemoveRequest(string requestId)
        {
            return recruitingState.RemoveRequest(requestId);
        }

        public void SetClassRoleSelectorState(RoleId roleId, bool isEnabled)
        {
            recruitingState.SetClassRoleSelectorState(roleId, isEnabled);
        }

        // == Requests Generator ==

        private void UpdateOffileRequests()
        {
            if (!guildState.IsExists || !IsEnabled.Value)
            {
                return;
            }

            TryRemoveOldRequests();

            // check next request time

            var currentTime = DateTime.Now;

            if (recruitingState.NextRequestTime > currentTime)
            {
                return;
            }

            // new requests

            var requestCount = recruitingState.Requests.Count;
            var maxRequestCount = CalcMaxRequestCount();

            if (requestCount >= maxRequestCount)
            {
                return;
            }

            var deltaTime = (int)(currentTime - recruitingState.NextRequestTime).TotalSeconds;
            var midRequestTime = (data.MaxNextRequestTime - data.MinNextRequestTime) / 2f;
            var possibleRequestCount = Mathf.RoundToInt(deltaTime / midRequestTime);

            maxRequestCount = Random.Range(requestCount, maxRequestCount + 1);

            var newRequestCount = Mathf.Min(maxRequestCount - requestCount, possibleRequestCount);

            for (var i = 0; i < newRequestCount; i++)
            {
                var seconds = Random.Range(0, deltaTime);
                var createTime = recruitingState.NextRequestTime.AddSeconds(seconds);

                var request = CreateRequest(createTime);

                recruitingState.AddRequest(request);
            }

            // next request time

            if (newRequestCount > 0)
            {
                ResetNextRequestTime(currentTime);
            }
        }

        private void ResetNextRequestTime(DateTime currentTime)
        {
            var randomRequestTime = Random.Range(data.MinNextRequestTime, data.MaxNextRequestTime);
            var nextRequestTime = currentTime.AddSeconds(randomRequestTime);

            recruitingState.SetNextRequestTime(nextRequestTime);
        }

        private void OnUpdate(TimeTick tick)
        {
            if (!guildState.IsExists || !IsEnabled.Value)
            {
                ResetNextRequestTime(DateTime.Now);

                return;
            }

            TryRemoveOldRequests();

            TryCreateNewRequests();
        }

        private void TryRemoveOldRequests()
        {
            var requestLifetime = data.RequestLifetime;

            var requests = recruitingState.Requests;
            var requestCount = requests.Count;

            var currentTime = DateTime.Now;

            for (var i = requestCount - 1; i >= 0; i--)
            {
                var request = requests[i];
                var removeTime = request.CreatedTime.AddSeconds(requestLifetime);

                if (request.IsDefault || removeTime > currentTime)
                {
                    continue;
                }

                recruitingState.RemoveRequest(request.Id);
            }
        }

        private void TryCreateNewRequests()
        {
            var requestCount = recruitingState.Requests.Count;
            var maxRequestCount = CalcMaxRequestCount();

            if (requestCount >= maxRequestCount)
            {
                return;
            }

            var currentTime = DateTime.Now;

            if (recruitingState.NextRequestTime > currentTime)
            {
                return;
            }

            var randomRequestTime = Random.Range(data.MinNextRequestTime, data.MaxNextRequestTime);
            var nextRequestTime = currentTime.AddSeconds(randomRequestTime);

            var request = CreateRequest(currentTime);

            recruitingState.AddRequest(request);
            recruitingState.SetNextRequestTime(nextRequestTime);
        }

        private int CalcMaxRequestCount()
        {
            var rosterCount = guildState.Characters.Count;
            var maxRosterCount = guildState.MaxCharactersCount.Value;

            var minRequestCount = data.MinRequestCount;
            var maxRequestCount = data.MaxRequestCount;

            var rosterRatio = (float)rosterCount / maxRosterCount;
            var addRequestCount = Mathf.RoundToInt((maxRequestCount - minRequestCount) * rosterRatio);

            return minRequestCount + addRequestCount;
        }

        // == Create Requests ==

        private void CreateDefaultRequests()
        {
            if (guildState.IsExists || recruitingState.Requests.Any())
            {
                return;
            }

            var defaultRequests = guildConfig.RecruitingParams.DefaultCharacters.Select(x =>
            {
                var classData = guildConfig.CharactersParams.GetClass(x.ClassId);
                var specData = guildConfig.CharactersParams.GetSpecialization(x.SpecId);

                return CreateRequest(classData, specData, DateTime.MinValue);
            });

            foreach (var request in defaultRequests)
            {
                recruitingState.AddRequest(request);
            }
        }

        private JoinRequestInfo CreateRequest(DateTime createdTime)
        {
            var roleId = recruitingState.ClassRoleSelectors
                .WeightedSelection(GetClassRoleWeight)
                .RoleId;

            var (classData, specData) = guildConfig.CharactersParams
                .GetSpecializations(roleId)
                .RandomValue();

            return CreateRequest(classData, specData, createdTime);
        }

        private JoinRequestInfo CreateRequest(ClassData classData, SpecializationData specData, DateTime createdTime)
        {
            var id = GuidUtils.Generate();
            var nickname = GetNickname(id);
            var recruitRank = guildState.GuildRanks.Last();

            var equipSlots = CreateEquipSlots(classData);

            CreateEquipItems(equipSlots, classData);

            var character = new CharacterInfo(id, nickname, classData.Id, specData.Id, equipSlots);

            character.Init();
            character.SetGuildRank(recruitRank.Id);

            return new JoinRequestInfo(character, createdTime);
        }

        private static string GetNickname(string id)
        {
            return $"Игрок ({id[..7]})";
        }

        private float GetClassRoleWeight(ClassRoleSelectorInfo classRoleSelector)
        {
            return classRoleSelector.IsEnabled.Value
                ? guildConfig.RecruitingParams.WeightSelectedRole
                : guildConfig.RecruitingParams.WeightUnselectedRole;
        }

        private IReadOnlyList<EquipSlotInfo> CreateEquipSlots(ClassData classData)
        {
            var equipsParams = inventoryConfig.EquipsParams;

            var equipSlots = equipsParams.Slots
                .Select(inventoryService.Factory.CreateSlot)
                .Cast<EquipSlotInfo>()
                .ToList();

            // маркировка слотов типом предмета

            var armorType = classData.ArmorType;
            var weaponType = classData.WeaponType;

            var armorGroup = (EquipGroup)equipsParams.GetGroup(armorType).Id;

            foreach (var equipSlot in equipSlots)
            {
                var equipGroup = equipSlot.EquipGroup;
                var equipType = equipGroup == armorGroup ? armorType : weaponType;

                equipSlot.SetEquipType(equipType);
            }

            return equipSlots;
        }

        private void CreateEquipItems(IReadOnlyList<EquipSlotInfo> equipSlots, ClassData classData)
        {
            // вычисление среднего уровня на основе среднего уровня текущих членов гильдии

            var recruitingData = guildConfig.RecruitingParams;
            var midEquipLevel = recruitingData.MinEquipLevel;

            var characterGroupsWeight = recruitingData.CharacterGroupsWeights;
            var characterGroupsCount = characterGroupsWeight.Count;

            var characters = guildState.Characters;
            var charactersCount = characters.Count;

            if (charactersCount != 0)
            {
                // сортировка гильдии от лучших к худшим по уровню предметов
                // формирование выборки из 3 групп
                // вычисление среднего уровня экипировки

                var charactersChunkSize = Mathf.Max(Mathf.CeilToInt(charactersCount / (float)characterGroupsCount), 1);

                var characterGroup = characters
                    .OrderByDescending(x => x.ItemsLevel.Value)
                    .Chunk(charactersChunkSize)
                    .Select((x, i) => (Group: x, Weight: characterGroupsWeight[i]))
                    .WeightedSelection(x => x.Weight)
                    .Group;

                midEquipLevel = characterGroup.Sum(x => x.ItemsLevel.Value) / characterGroup.Length;
                midEquipLevel = Mathf.Max(midEquipLevel, recruitingData.MinEquipLevel);
            }

            // пул предметов доступных для персонажа
            // TODO: сформировать пул на основе прогресса пресонажа (по данжам, по рейдам) + крафт

            var armorType = classData.ArmorType;
            var weaponType = classData.WeaponType;
            var equipsParams = inventoryConfig.EquipsParams;

            var equipItemsPool = equipsParams.Items
                .Where(x => x.Type == armorType || x.Type == weaponType)
                .ToListPool();

            // перетасовка слотов для генерации предметов в них

            var shuffledSlots = equipSlots.Shuffle();

            // 1 этап: генерация первых слотов +5/-10 уровней

            var firstPhaseData = recruitingData.FirstPhase;
            var firstPhaseSlotsCount = Random.Range(firstPhaseData.MinEquipCount, firstPhaseData.MaxEquipCount + 1);
            var firstPhaseSlots = shuffledSlots.Take(firstPhaseSlotsCount);

            CreatePhaseItems(firstPhaseData, equipItemsPool, firstPhaseSlots, midEquipLevel);

            // 2 этап: генерация последжних слотов +10/-5 уровней

            var lastPhaseData = recruitingData.LastPhase;
            var lastPhaseSlotsCount = equipSlots.Count - firstPhaseSlotsCount;
            var lastPhaseSlots = shuffledSlots.Skip(firstPhaseSlotsCount).Take(lastPhaseSlotsCount);

            CreatePhaseItems(lastPhaseData, equipItemsPool, lastPhaseSlots, midEquipLevel);

            shuffledSlots.ReleaseListPool();
            equipItemsPool.ReleaseListPool();
        }

        private void CreatePhaseItems(
            EquipGeneratorPhaseData phaseData,
            IReadOnlyList<EquipItemData> equipItems,
            IEnumerable<EquipSlotInfo> equipSlots,
            int midEquipLevel)
        {
            var minLevel = midEquipLevel - phaseData.MinEquipLevel;
            var maxLevel = midEquipLevel + phaseData.MaxEquipLevel;

            var filteredEquips = equipItems
                .Where(x => x.Level >= minLevel && x.Level <= maxLevel)
                .ToListPool();

            foreach (var equipSlot in equipSlots)
            {
                var slotType = equipSlot.Slot;

                var equipData = filteredEquips
                    .Where(x => x.Slot == slotType)
                    .RandomValue();

                var equipItem = inventoryService.Factory.CreateItem(equipData);

                equipSlot.SetItem(equipItem as EquipItemInfo);
            }

            filteredEquips.ReleaseListPool();
        }
    }
}