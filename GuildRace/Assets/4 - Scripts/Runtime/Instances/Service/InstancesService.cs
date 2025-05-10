using System.Collections.Generic;
using System.Linq;
using AD.Services;
using AD.Services.AppEvents;
using AD.Services.ProtectedTime;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Guild;
using Game.Inventory;
using UniRx;
using VContainer;

namespace Game.Instances
{
    public class InstancesService : Service, IInstancesService
    {
        private readonly InstancesState state;

        private readonly InstanceModule instanceModule;
        private readonly ActiveInstanceModule activeInstanceModule;

        private readonly Dictionary<int, RewardHandler> rewardHandlers;
        private readonly Dictionary<int, ConsumableMechanicHandler> consumableHandlers;

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

            instanceModule = new(state, guildConfig, instancesConfig, router, guildService, inventoryService, this);
            activeInstanceModule = new(this, state, time);

            rewardHandlers = instancesConfig.RewardsParams.RewardHandlers.ToDictionary(x => x.Id, x => x);
            consumableHandlers = instancesConfig.ConsumablesParams.MechanicHandlers.ToDictionary(x => x.Id, x => x);
        }

        public override async UniTask<bool> Init()
        {
            state.Init();

            InitHandlers();

            appEvents.AddAppTickListener(activeInstanceModule);

            return await Inited();
        }

        // == Handlers ==

        private void InitHandlers()
        {
            rewardHandlers.Values.ForEach(handler =>
            {
                resolver.Inject(handler);
            });

            consumableHandlers.Values.ForEach(handler =>
            {
                resolver.Inject(handler);
            });
        }

        public RewardHandler GetRewardHandler(int id)
        {
            return rewardHandlers[id];
        }

        public ConsumableMechanicHandler GetConsumableHandler(int id)
        {
            return consumableHandlers[id];
        }

        // == Instance ==

        public async UniTask StartSetupInstance(SetupInstanceArgs args)
        {
            await instanceModule.StartSetupInstance(args);
        }

        public IReadOnlyCollection<SquadCandidateInfo> GetSquadCandidates()
        {
            return instanceModule.GetSquadCandidates();
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

        public int CompleteActiveInstance(string activeInstanceId)
        {
            return instanceModule.CompleteActiveInstance(activeInstanceId);
        }

        // == Dispose ==

        public override void Dispose()
        {
            base.Dispose();

            CancelSetupInstance();

            appEvents.RemoveAppTickListener(activeInstanceModule);
        }
    }
}