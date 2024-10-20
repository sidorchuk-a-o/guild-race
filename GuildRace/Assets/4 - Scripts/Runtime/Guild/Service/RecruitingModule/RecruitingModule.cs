using AD.Services.ProtectedTime;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Items;
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
        private readonly RecruitingModuleData data;
        private readonly ItemsDatabaseConfig itemsConfig;

        private readonly GuildState guildState;
        private readonly RecruitingState recruitingState;

        private readonly IItemsDatabaseService itemsService;
        private readonly ITimeService time;

        public IReadOnlyReactiveProperty<bool> IsEnabled => recruitingState.IsEnabled;

        public IJoinRequestsCollection Requests => recruitingState.Requests;
        public IReadOnlyList<ClassRoleSelectorInfo> ClassRoleSelectors => recruitingState.ClassRoleSelectors;

        public RecruitingModule(
            GuildState guildState,
            GuildConfig guildConfig,
            ItemsDatabaseConfig itemsConfig,
            IItemsDatabaseService itemsService,
            ITimeService time,
            IObjectResolver resolver)
        {
            this.guildConfig = guildConfig;
            this.guildState = guildState;
            this.itemsConfig = itemsConfig;
            this.itemsService = itemsService;
            this.time = time;

            data = guildConfig.RecruitingModule;
            recruitingState = new(guildConfig, itemsService, resolver);
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
            if (!IsEnabled.Value)
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

            var deltaTime = (int)(currentTime - recruitingState.NextRequestTime).TotalSeconds;
            var midRequestTime = (data.MaxNextRequestTime - data.MinNextRequestTime) / 2f;
            var possibleRequestCount = Mathf.RoundToInt(deltaTime / midRequestTime);

            var requestCount = recruitingState.Requests.Count;
            var maxRequestCount = Random.Range(requestCount, CalcMaxRequestCount());

            var newRequestCount = Mathf.Min(maxRequestCount - requestCount, possibleRequestCount);

            for (var i = 0; i < newRequestCount; i++)
            {
                var seconds = Random.Range(0, deltaTime);
                var createTime = recruitingState.NextRequestTime.AddSeconds(seconds);

                CreateRequest(createTime);
            }

            // next request time

            if (newRequestCount > 0)
            {
                var randomRequestTime = Random.Range(data.MinNextRequestTime, data.MaxNextRequestTime);
                var nextRequestTime = currentTime.AddSeconds(randomRequestTime);

                recruitingState.SetNextRequestTime(nextRequestTime);
            }
        }

        private void OnUpdate(TimeTick tick)
        {
            if (!IsEnabled.Value)
            {
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
            var maxRosterCount = guildConfig.MaxCharactersCount;

            var minRequestCount = data.MinRequestCount;
            var maxRequestCount = data.MaxRequestCount;

            var rosterRatio = (float)rosterCount / maxRosterCount;
            var addRequestCount = Mathf.RoundToInt((maxRequestCount - minRequestCount) * rosterRatio);

            return minRequestCount + addRequestCount;
        }

        // == Create Requests ==

        private void CreateDefaultRequests()
        {
            if (guildState.IsExists)
            {
                return;
            }

            var defaultRequests = guildConfig.RecruitingModule.DefaultCharacters.Select(x =>
            {
                var classData = guildConfig.CharactersModule.GetClass(x.ClassId);
                var specData = guildConfig.CharactersModule.GetSpecialization(x.SpecId);

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

            var (classData, specData) = guildConfig.CharactersModule
                .GetSpecializations(roleId)
                .RandomValue();

            return CreateRequest(classData, specData, createdTime);
        }

        private JoinRequestInfo CreateRequest(ClassData classData, SpecializationData specData, DateTime createdTime)
        {
            var id = GuidUtils.Generate();
            var nickname = GetNickname(id);

            var recruitRank = guildState.GuildRanks.Last();
            var equipSlots = itemsService.CreateDefaultSlots();

            CreateEquipItems(equipSlots, classData);

            var character = new CharacterInfo(id, nickname, classData.Id, equipSlots);

            character.SetGuildRank(recruitRank.Id);
            character.SetSpecialization(specData.Id);

            return new JoinRequestInfo(character, createdTime);
        }

        private static string GetNickname(string id)
        {
            return $"Игрок ({id[..7]})";
        }

        private float GetClassRoleWeight(ClassRoleSelectorInfo classRoleSelector)
        {
            return classRoleSelector.IsEnabled.Value
                ? guildConfig.RecruitingModule.WeightSelectedRole
                : guildConfig.RecruitingModule.WeightUnselectedRole;
        }

        private void CreateEquipItems(IEquipSlotsCollection equipSlots, ClassData classData)
        {
            // вычисление среднего уровня на основе среднего уровня текущих членов гильдии

            var recruitingData = guildConfig.RecruitingModule;
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
            }

            // пул предметов доступных для персонажа
            // TODO: сформировать пул на основе прогресса пресонажа (по данжам, по рейдам) + крафт

            var armorType = classData.ArmorType;
            var weaponType = classData.WeaponType;

            var equipItemsPool = itemsConfig.EquipsParams.Items
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
            IReadOnlyList<EquipItemData> equipsPool,
            IEnumerable<EquipSlotInfo> equipSlots,
            int midEquipLevel)
        {
            var minLevel = midEquipLevel - phaseData.MinEquipLevel;
            var maxLevel = midEquipLevel + phaseData.MaxEquipLevel;

            var firstPhaseItems = equipsPool
                .Where(x => x.Level >= minLevel && x.Level <= maxLevel)
                .ToListPool();

            foreach (var equipSlot in equipSlots)
            {
                var slotType = equipSlot.Slot;

                var equipData = firstPhaseItems
                    .Where(x => x.Slot == slotType)
                    .RandomValue();

                var equipItem = itemsService.CreateItemInfo(equipData);

                equipSlot.SetItem(equipItem as EquipItemInfo);
            }

            firstPhaseItems.ReleaseListPool();
        }
    }
}