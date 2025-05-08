using AD.Services.Router;
using Game.Instances;
using Game.Inventory;
using System.Collections.Generic;
using System.Linq;
using VContainer;

namespace Game.Guild
{
    public class GuildVMFactory : VMFactory
    {
        private readonly GuildConfig guildConfig;

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
            IObjectResolver resolver)
        {
            this.guildConfig = guildConfig;
            this.guildService = guildService;
            this.resolver = resolver;
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

            return new ClassVM(classData);
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