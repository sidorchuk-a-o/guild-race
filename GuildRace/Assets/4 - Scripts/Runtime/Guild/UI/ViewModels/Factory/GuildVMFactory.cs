using AD.Services.Pools;
using AD.Services.Router;
using Game.Instances;
using Game.Inventory;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using static TMPro.TMP_Dropdown;

namespace Game.Guild
{
    public class GuildVMFactory : VMFactory
    {
        private readonly GuildConfig guildConfig;
        private readonly PoolContainer<Sprite> imagesPool;

        private readonly IGuildService guildService;
        private readonly IObjectResolver resolver;

        private InventoryVMFactory inventoryVMF;
        private InstancesVMFactory instancesVMF;

        public int MaxCharactersCount => guildConfig.MaxCharactersCount;

        public InventoryVMFactory InventoryVMF => inventoryVMF ??= resolver.Resolve<InventoryVMFactory>();
        public InstancesVMFactory InstancesVMF => instancesVMF ??= resolver.Resolve<InstancesVMFactory>();

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

        public UniTask<Sprite> RentImage(AssetReference imageRef, CancellationToken token)
        {
            return imagesPool.RentAsync(imageRef, token: token);
        }

        // == View Models ==

        public GuildVM GetGuild()
        {
            return new GuildVM(guildConfig, guildService);
        }

        public CharacterVM GetCharacter(string characterId)
        {
            var character = guildService.Characters[characterId];

            return new CharacterVM(character, this);
        }

        public CharactersVM GetRoster()
        {
            return new CharactersVM(guildService.Characters, this);
        }

        public RecruitingVM GetRecruiting()
        {
            return new RecruitingVM(guildService.RecruitingModule, this);
        }

        public JoinRequestsVM GetJoinRequests()
        {
            return new JoinRequestsVM(guildService.RecruitingModule.Requests, this);
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

            return new RoleVM(roleData);
        }

        public SubRoleVM GetRole(SubRoleId subRoleId)
        {
            var subRoleData = guildConfig.CharactersParams.GetSubRole(subRoleId);

            return new SubRoleVM(subRoleData);
        }

        public ClassVM GetClass(ClassId classId)
        {
            var classData = guildConfig.CharactersParams.GetClass(classId);

            return new ClassVM(classData, this);
        }

        public SpecializationVM GetSpecialization(SpecializationId specId)
        {
            var specData = guildConfig.CharactersParams.GetSpecialization(specId);

            return new SpecializationVM(specData, this);
        }

        public GuildRankVM GetGuildRank(GuildRankId guildRankId)
        {
            var guildRank = guildService.GuildRanks[guildRankId];

            return new GuildRankVM(guildRank);
        }

        public GuildRanksVM GetGuildRanks()
        {
            var guildRanks = guildService.GuildRanks;

            return new GuildRanksVM(guildRanks);
        }

        public List<OptionData> GetGuildRanksOptions()
        {
            return guildService.GuildRanks.Skip(1)
                .Select(x => new OptionData(x.Name.Value))
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
            return new GuildBankTabsVM(guildService.BankTabs, InventoryVMF);
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
    }
}