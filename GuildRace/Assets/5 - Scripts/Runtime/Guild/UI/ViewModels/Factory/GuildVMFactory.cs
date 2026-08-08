using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AD.Game.Instances;
using AD.Game.Inventory;
using AD.Localization;
using AD.Pools;
using AD.Router;
using AD.Store;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using static TMPro.TMP_Dropdown;

namespace AD.Game.Guild
{
    public class GuildVMFactory : VMFactory
    {
        private readonly GuildConfig guildConfig;
        private readonly PoolContainer<Sprite> imagesPool;

        private readonly IGuildService guildService;
        private readonly IObjectResolver resolver;

        private InventoryVMFactory inventoryVMF;
        private InstancesVMFactory instancesVMF;
        private StoreVMFactory storeVMF;

        public ReadOnlyReactiveProperty<int> MaxCharactersCount => guildService.MaxCharactersCount;

        public InventoryVMFactory InventoryVMF => inventoryVMF ??= resolver.Resolve<InventoryVMFactory>();
        public InstancesVMFactory InstancesVMF => instancesVMF ??= resolver.Resolve<InstancesVMFactory>();
        public StoreVMFactory StoreVMF => storeVMF ??= resolver.Resolve<StoreVMFactory>();

        public GuildVMFactory(
            GuildConfig guildConfig,
            IGuildService guildService,
            IPoolsService poolsService,
            IObjectResolver resolver)
        {
            this.guildConfig = guildConfig;
            this.guildService = guildService;
            this.resolver = resolver;

            imagesPool = poolsService.CreateAssetPool<Sprite>();
        }

        public UniTask<Sprite> RentImage(AssetReference imageRef, CancellationTokenSource ct)
        {
            return imagesPool.RentAsync(imageRef, token: ct.Token);
        }

        public ItemCounterVM CreateItemCounter(int itemDataId)
        {
            var bankGrids = guildService.BankTabs.Select(x => x.Grid);

            return InventoryVMF.CreateItemCounter(itemDataId, bankGrids);
        }

        // == View Models ==

        public GuildVM GetGuild()
        {
            return new(guildService, this);
        }

        public EmblemVM GetEmblem(EmblemInfo emblem)
        {
            return new(emblem, guildConfig.EmblemParams);
        }

        public CharacterVM GetCharacter(string characterId)
        {
            var character = guildService.Characters[characterId];

            return new(character, this);
        }

        public CharactersVM GetRoster()
        {
            return new(guildService.Characters, this);
        }

        public RecruitingVM GetRecruiting()
        {
            return new(guildService.RecruitingModule, this);
        }

        public JoinRequestsVM GetJoinRequests()
        {
            return new(guildService.RecruitingModule.Requests, this);
        }

        public IReadOnlyList<ClassRoleSelectorVM> GetClassRoleSelectors()
        {
            return guildService.RecruitingModule.ClassRoleSelectors
                .Select(x => new ClassRoleSelectorVM(x, this))
                .ToList();
        }

        public RoleVM GetRole(RoleId roleId)
        {
            var roleData = guildConfig.CharactersParams.GetRole(roleId);

            return new(roleData, this);
        }

        public SubRoleVM GetSubRole(SubRoleId subRoleId)
        {
            var subRoleData = guildConfig.CharactersParams.GetSubRole(subRoleId);

            return new(subRoleData, this);
        }

        public ClassVM GetClass(ClassId classId)
        {
            var classData = guildConfig.CharactersParams.GetClass(classId);

            return new(classData, this);
        }

        public SpecializationVM GetSpecialization(SpecializationId specId)
        {
            var specData = guildConfig.CharactersParams.GetSpecialization(specId);

            return new(specData, this);
        }

        public GuildRankVM GetGuildRank(GuildRankId guildRankId)
        {
            var guildRank = guildService.GuildRanks[guildRankId];

            return new(guildRank);
        }

        public GuildRanksVM GetGuildRanks()
        {
            var guildRanks = guildService.GuildRanks;

            return new(guildRanks);
        }

        public List<OptionData> GetGuildRanksOptions()
        {
            return guildService.GuildRanks.Skip(1)
                .Select(x => new OptionData((LocalizeText)x.NameKey))
                .ToList();
        }

        public int GetGuildRankIndex(GuildRankId rankId)
        {
            return guildService.GuildRanks.FindIndex(x => x.Id == rankId);
        }

        public void UpdateGuildRank(string characterId, int rankIndex)
        {
            guildService.UpdateGuildRank(characterId, rankIndex);
        }

        // == Bank ==

        public GuildBankTabsVM GetGuildBankTabs()
        {
            return new(guildService.BankTabs, InventoryVMF);
        }

        // == Common Methods ==

        public void CreateOrUpdateGuild(GuildEM guildEM)
        {
            guildService.CreateOrUpdateGuild(guildEM);
        }

        public int RemoveCharacter(string characterId)
        {
            return guildService.RemoveCharacter(characterId);
        }

        public int AcceptJoinRequest(string requestId)
        {
            return guildService.AcceptJoinRequest(requestId);
        }

        public int DeclineJoinRequest(string requestId)
        {
            return guildService.DeclineJoinRequest(requestId);
        }

        public void SetClassRoleSelectorState(RoleId roleId, bool isEnabled)
        {
            guildService.SetClassRoleSelectorState(roleId, isEnabled);
        }

        // == Leaderboard ==

        public GuildScoreVM GetLeaderboardExtra(string extraData)
        {
            var data = guildService.GetLeaderboardExtraData(extraData);

            return new(data, this);
        }
    }
}