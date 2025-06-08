using AD.Services.Pools;
using AD.Services.ProtectedTime;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Guild;
using Game.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Game.Instances
{
    public class InstancesVMFactory : VMFactory
    {
        private readonly IInstancesService instancesService;
        private readonly IObjectResolver resolver;

        private readonly GameObject poolsContainer;
        private readonly PoolContainer<Sprite> imagesPool;
        private readonly PoolContainer<GameObject> objectsPool;

        private GuildVMFactory guildVMF;
        private InventoryVMFactory inventoryVMF;

        private Dictionary<Type, ConsumableMechanicVMFactory> mechanicFactoriesDict;

        public ITimeService TimeService { get; }
        public InstancesConfig InstancesConfig { get; }

        public GuildVMFactory GuildVMF => guildVMF ??= resolver.Resolve<GuildVMFactory>();
        public InventoryVMFactory InventoryVMF => inventoryVMF ??= resolver.Resolve<InventoryVMFactory>();

        public InstancesVMFactory(
            InstancesConfig instancesConfig,
            IInstancesService instancesService,
            IPoolsService poolsService,
            ITimeService timeService,
            IObjectResolver resolver)
        {
            this.instancesService = instancesService;
            this.resolver = resolver;

            TimeService = timeService;
            InstancesConfig = instancesConfig;

            if (poolsContainer == null)
            {
                poolsContainer = new("--- Instances Pools ---");
                poolsContainer.DontDestroyOnLoad();

                imagesPool = poolsService.CreateAssetPool<Sprite>();
                objectsPool = poolsService.CreatePrefabPool<GameObject>(poolsContainer.transform);
            }
        }

        // == Pools ==

        public UniTask<Sprite> LoadImage(AssetReference imageRef, CancellationTokenSource ct)
        {
            return imagesPool.RentAsync(imageRef, token: ct.Token);
        }

        public UniTask<GameObject> RentObjectAsync(AssetReference objectRef, CancellationToken token = default)
        {
            return objectsPool.RentAsync(objectRef, token: token);
        }

        public void ReturnObject<TComponent>(TComponent instance) where TComponent : Component
        {
            ReturnObject(instance.gameObject);
        }

        public void ReturnObject(GameObject instance)
        {
            objectsPool.Return(instance);
        }

        // == Consumables ==

        public void SetConsumableMechanicFactories(List<ConsumableMechanicVMFactory> mechanicFactories)
        {
            mechanicFactoriesDict = mechanicFactories.ToDictionary(x => x.Type, x => x);
            mechanicFactoriesDict.ForEach(x => resolver.Inject(x.Value));
        }

        public ConsumableMechanicVM GetConsumableMechanic(ConsumablesItemInfo info)
        {
            var handler = instancesService.GetConsumableHandler(info.MechanicId);
            var factory = mechanicFactoriesDict[handler.GetType()];

            return factory.GetValue(info, handler, this);
        }

        // == Threats ==

        public ThreatDataVM GetThreat(ThreatId threatId)
        {
            return new ThreatDataVM(InstancesConfig.GetThreat(threatId), this);
        }

        // == Season ==

        public SeasonVM GetFirstSeason()
        {
            var firstSeason = instancesService.Seasons.FirstOrDefault();

            return new SeasonVM(firstSeason, this);
        }

        // == Instance ==

        public InstanceVM GetInstance(int instanceId)
        {
            var instance = instancesService.Seasons.GetInstance(instanceId);

            return GetInstance(instance);
        }

        public InstanceVM GetInstance(InstanceInfo instance)
        {
            return new InstanceVM(instance, this);
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

        public int CompleteActiveInstance(string activeInstanceId)
        {
            return instancesService.CompleteActiveInstance(activeInstanceId);
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

        public int GetMaxCompletedCount(int instanceId)
        {
            var instance = instancesService.Seasons.GetInstance(instanceId);
            var cooldownParams = InstancesConfig.GetUnitCooldown(instance.Type);

            return cooldownParams != null
                ? cooldownParams.MaxCompletedCount
                : 0;
        }

        public bool CheckUnitCooldown(int unitId, int instanceId)
        {
            return instancesService.CheckUnitCooldown(unitId, instanceId);
        }
    }
}