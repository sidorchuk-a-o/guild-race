using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Guild;
using Game.Inventory;
using System.Collections.Generic;
using System.Linq;
using VContainer;

namespace Game.Instances
{
    public class InstancesVMFactory : VMFactory
    {
        private readonly GuildConfig guildConfig;

        private readonly IInstancesService instancesService;
        private readonly IGuildService guildService;
        private readonly IObjectResolver resolver;

        private GuildVMFactory guildVMF;
        private InventoryVMFactory inventoryVMF;

        public InventoryVMFactory InventoryVMF => inventoryVMF ??= resolver.Resolve<InventoryVMFactory>();
        public GuildVMFactory GuildVMF => guildVMF ??= resolver.Resolve<GuildVMFactory>();

        public InstancesVMFactory(
            IInstancesService instancesService,
            IGuildService guildService,
            GuildConfig guildConfig,
            IObjectResolver resolver)
        {
            this.instancesService = instancesService;
            this.guildService = guildService;
            this.guildConfig = guildConfig;
            this.resolver = resolver;
        }

        // == Season ==

        public SeasonVM GetFirstSeason()
        {
            var firstSeason = instancesService.Seasons.FirstOrDefault();

            return new SeasonVM(firstSeason);
        }

        // == Instance ==

        public InstanceVM GetInstance(int instanceId)
        {
            var instance = instancesService.Seasons.GetInstance(instanceId);

            return GetInstance(instance);
        }

        public InstanceVM GetInstance(InstanceInfo instance)
        {
            return new InstanceVM(instance);
        }

        public ActiveInstanceVM GetActiveInstance(string activeInstanceId)
        {
            if (activeInstanceId.IsNullOrEmpty())
            {
                return null;
            }

            var activeInstance = FindActiveInstance(activeInstanceId);

            return activeInstance != null
                ? new ActiveInstanceVM(activeInstance, this, InventoryVMF)
                : null;
        }

        public ActiveInstancesVM GetActiveInstances()
        {
            return new ActiveInstancesVM(instancesService.ActiveInstances, this, InventoryVMF);
        }

        private ActiveInstanceInfo FindActiveInstance(string activeInstanceId)
        {
            var setupInstance = instancesService.SetupInstance;

            if (setupInstance != null && activeInstanceId == setupInstance.Id)
            {
                return setupInstance;
            }
            else
            {
                return instancesService.ActiveInstances.GetInstance(activeInstanceId);
            }
        }

        // == Setup Instance ==

        public async UniTask StartSetupInstance(int instanceId)
        {
            await instancesService.StartSetupInstance(instanceId);
        }

        public void TryAddCharacterToSquad(string characterId)
        {
            instancesService.TryAddCharacterToSquad(characterId);
        }

        public void TryRemoveCharacterFromSquad(string characterId)
        {
            instancesService.TryRemoveCharacterFromSquad(characterId);
        }

        public async UniTask CompleteSetupAndStartInstance()
        {
            await instancesService.CompleteSetupAndStartInstance();
        }

        public void CancelSetupInstance()
        {
            instancesService.CancelSetupInstance();
        }

        public int StopActiveInstance(string activeInstanceId)
        {
            return instancesService.StopActiveInstance(activeInstanceId);
        }

        public ActiveInstanceVM GetSetupInstance()
        {
            var activeInstance = instancesService.SetupInstance;

            return new ActiveInstanceVM(activeInstance, this, InventoryVMF);
        }

        // == Characters ==

        public IReadOnlyList<RoleTabVM> GetCharactersByRoles()
        {
            var roles = guildConfig.CharactersParams.Roles;
            var characters = guildService.Characters;

            var charactersByRole = characters
                .GroupBy(GetCharacterRole)
                .Select(x => (roleId: x.Key, characters: CreateCollection(x)))
                .ToListPool();

            var roleTabsVM = roles
                .Select(role => GetRoleTab(role, charactersByRole))
                .ToList();

            charactersByRole.ReleaseListPool();

            return roleTabsVM;
        }

        private RoleId GetCharacterRole(CharacterInfo info)
        {
            var charactersParams = guildConfig.CharactersParams;
            var specData = charactersParams.GetSpecialization(info.SpecId.Value);

            return specData.RoleId;
        }

        private static CharactersCollection CreateCollection(IEnumerable<CharacterInfo> characters)
        {
            return new CharactersCollection(characters.OrderBy(x => x.HasInstance));
        }

        private RoleTabVM GetRoleTab(RoleData role, List<(RoleId roleId, CharactersCollection characters)> charactersByRole)
        {
            var characters = charactersByRole.FirstOrDefault(x => x.roleId == role.Id).characters;
            var charactersVM = new CharactersVM(characters ?? new(null), GuildVMF, InventoryVMF, this);

            return new RoleTabVM(role, charactersVM);
        }
    }
}