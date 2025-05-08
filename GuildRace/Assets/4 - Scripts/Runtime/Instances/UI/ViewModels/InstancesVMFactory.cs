using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Guild;
using Game.Inventory;
using System.Linq;
using VContainer;

namespace Game.Instances
{
    public class InstancesVMFactory : VMFactory
    {
        private readonly InstancesConfig instancesConfig;
        private readonly IInstancesService instancesService;
        private readonly IObjectResolver resolver;

        private GuildVMFactory guildVMF;
        private InventoryVMFactory inventoryVMF;

        public InstancesConfig InstancesConfig => instancesConfig;

        public GuildVMFactory GuildVMF => guildVMF ??= resolver.Resolve<GuildVMFactory>();
        public InventoryVMFactory InventoryVMF => inventoryVMF ??= resolver.Resolve<InventoryVMFactory>();

        public InstancesVMFactory(
            InstancesConfig instancesConfig,
            IInstancesService instancesService,
            IObjectResolver resolver)
        {
            this.instancesService = instancesService;
            this.instancesConfig = instancesConfig;
            this.resolver = resolver;
        }

        // == Consumables ==

        public ConsumableMechanicVM GetConsumableMechanic(int mechanicId)
        {
            var mechanic = instancesService.GetMechanicHandler(mechanicId);

            return new ConsumableMechanicVM(mechanic);
        }

        // == Season ==

        public SeasonVM GetFirstSeason()
        {
            var firstSeason = instancesService.Seasons.FirstOrDefault();

            return new SeasonVM(firstSeason, instancesConfig);
        }

        // == Instance ==

        public InstanceVM GetInstance(int instanceId)
        {
            var instance = instancesService.Seasons.GetInstance(instanceId);

            return GetInstance(instance);
        }

        public InstanceVM GetInstance(InstanceInfo instance)
        {
            return new InstanceVM(instance, instancesConfig);
        }

        public ActiveInstanceVM GetActiveInstance(string activeInstanceId)
        {
            if (activeInstanceId.IsNullOrEmpty())
            {
                return null;
            }

            var activeInstance = FindActiveInstance(activeInstanceId);

            return activeInstance != null
                ? new ActiveInstanceVM(activeInstance, this)
                : null;
        }

        public ActiveInstancesVM GetActiveInstances()
        {
            return new ActiveInstancesVM(instancesService.ActiveInstances, this);
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

        public async UniTask StartSetupInstance(SetupInstanceArgs args)
        {
            await instancesService.StartSetupInstance(args);
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

            return new ActiveInstanceVM(activeInstance, this);
        }

        public SquadCandidatesVM GetSquadCandidates()
        {
            var candidates = instancesService.GetSquadCandidates();

            return new SquadCandidatesVM(candidates, this);
        }

        public UnitVM GetUnit(UnitInfo unit)
        {
            return new UnitVM(unit, instancesConfig);
        }
    }
}