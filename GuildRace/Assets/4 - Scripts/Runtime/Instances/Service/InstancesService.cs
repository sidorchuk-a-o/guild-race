using System.Collections.Generic;
using System.Linq;
using AD.Services;
using AD.Services.AppEvents;
using AD.Services.ProtectedTime;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using Game.Guild;
using Game.Inventory;
using VContainer;

namespace Game.Instances
{
    public class InstancesService : Service, IInstancesService
    {
        private readonly InstancesState state;

        private readonly InstanceModule instanceModule;
        private readonly ActiveInstanceModule activeInstanceModule;

        private readonly List<ConsumableMechanicHandler> mechanicHandlers;

        private readonly IAppEventsService appEvents;
        private readonly IObjectResolver resolver;

        public ISeasonsCollection Seasons => state.Seasons;
        public IActiveInstancesCollection ActiveInstances => state.ActiveInstances;

        public ActiveInstanceInfo SetupInstance => state.SetupInstance;

        public InstancesService(
            GuildConfig guildConfig,
            InstancesConfig instancesConfig,
            IGuildService guildService,
            IInventoryService inventoryService,
            IRouterService router,
            IAppEventsService appEvents,
            ITimeService time,
            IObjectResolver resolver)
        {
            this.appEvents = appEvents;
            this.resolver = resolver;

            state = new(instancesConfig, time, guildService, inventoryService, resolver);

            instanceModule = new(state, guildConfig, instancesConfig, router, guildService, inventoryService);
            activeInstanceModule = new(instancesConfig, this, time);

            mechanicHandlers = instancesConfig.ConsumablesParams.MechanicHandlers.ToList();
        }

        public override async UniTask<bool> Init()
        {
            state.Init();
            InitMechanicHandlers();

            appEvents.AddAppTickListener(activeInstanceModule);

            return await Inited();
        }

        // == Consumable Mechanics ==

        private void InitMechanicHandlers()
        {
            mechanicHandlers.ForEach(handler =>
            {
                resolver.Inject(handler);

                handler.Init();
            });
        }

        public ConsumableMechanicHandler GetMechanicHandler(int id)
        {
            return mechanicHandlers.FirstOrDefault(x => x.Id == id);
        }

        // == Instance ==

        public async UniTask StartSetupInstance(int instanceId)
        {
            await instanceModule.StartSetupInstance(instanceId);
        }

        public void TryAddCharacterToSquad(string characterId)
        {
            instanceModule.TryAddCharacterToSquad(characterId);
        }

        public void TryRemoveCharacterFromSquad(string characterId)
        {
            instanceModule.TryRemoveCharacterFromSquad(characterId);
        }

        public async UniTask CompleteSetupAndStartInstance()
        {
            await instanceModule.CompleteSetupAndStartInstance();
        }

        public void CancelSetupInstance()
        {
            instanceModule.CancelSetupInstance();
        }

        public int StopActiveInstance(string activeInstanceId)
        {
            return instanceModule.StopActiveInstance(activeInstanceId);
        }

        // == Dispose ==

        public override void Dispose()
        {
            base.Dispose();

            appEvents.RemoveAppTickListener(activeInstanceModule);
        }
    }
}