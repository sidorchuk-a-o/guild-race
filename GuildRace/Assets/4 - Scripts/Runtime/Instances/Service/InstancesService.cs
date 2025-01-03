﻿using AD.Services;
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

        private readonly IAppEventsService appEvents;

        public ISeasonsCollection Seasons => state.Seasons;
        public IActiveInstancesCollection ActiveInstances => state.ActiveInstances;

        public bool HasPlayerInstance => state.HasPlayerInstance;
        public ActiveInstanceInfo SetupInstance => state.SetupInstance;
        public ActiveInstanceInfo PlayerInstance => state.PlayerInstance;

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

            state = new(instancesConfig, time, inventoryService, resolver);

            instanceModule = new(state, guildConfig, instancesConfig, router, guildService, inventoryService);
            activeInstanceModule = new(instancesConfig, this, time);
        }

        public override async UniTask<bool> Init()
        {
            state.Init();

            appEvents.AddAppTickListener(activeInstanceModule);

            return await Inited();
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

        public async UniTask CompleteSetupAndStartInstance(bool playerInstance)
        {
            await instanceModule.CompleteSetupAndStartInstance(playerInstance);
        }

        public void CancelSetupInstance()
        {
            instanceModule.CancelSetupInstance();
        }

        public async UniTask StartPlayerInstance()
        {
            await instanceModule.StartPlayerInstance();
        }

        public async UniTask StopPlayerInstance()
        {
            await instanceModule.StopPlayerInstance();
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