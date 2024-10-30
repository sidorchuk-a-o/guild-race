using AD.Services.Router;
using Game.Inventory;
using System.Collections.Generic;
using System.Linq;

namespace Game.Guild
{
    public class GuildVMFactory : VMFactory
    {
        private readonly GuildConfig guildConfig;
        private readonly IGuildService guildService;
        private readonly InventoryVMFactory inventoryVMF;

        public int MaxCharactersCount => guildConfig.MaxCharactersCount;

        public GuildVMFactory(
            GuildConfig guildConfig,
            IGuildService guildService,
            InventoryVMFactory inventoryVMF)
        {
            this.guildConfig = guildConfig;
            this.guildService = guildService;
            this.inventoryVMF = inventoryVMF;
        }

        // == View Models ==

        public GuildVM GetGuild()
        {
            return new GuildVM(guildConfig, guildService);
        }

        public GuildBankTabsVM GetGuildBankTabs()
        {
            return new GuildBankTabsVM(guildService.BankTabs, inventoryVMF);
        }

        public CharactersVM GetRoster()
        {
            return new CharactersVM(guildService.Characters, this, inventoryVMF);
        }

        public RecruitingVM GetRecruiting()
        {
            return new RecruitingVM(guildService.RecruitingModule, this);
        }

        public JoinRequestsVM GetJoinRequests()
        {
            return new JoinRequestsVM(guildService.RecruitingModule.Requests, this, inventoryVMF);
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