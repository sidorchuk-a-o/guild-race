using AD.Services;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Game.Instances
{
    public class InstancesService : Service, IInstancesService
    {
        private readonly InstancesState state;
        private readonly InstanceModule instanceModule;

        public ISeasonsCollection Seasons => state.Seasons;

        public bool HasCurrentInstance => state.HasCurrentInstance;
        public InstanceInfo CurrentInstance => state.CurrentInstance;

        public InstancesService(InstancesConfig config, IRouterService router, IObjectResolver resolver)
        {
            state = new(config, resolver);
            instanceModule = new(state, router);
        }

        public override async UniTask<bool> Init()
        {
            state.Init();

            return await Inited();
        }

        // == Instance ==

        public async UniTask StartInstance(int instanceId)
        {
            await instanceModule.StartInstance(instanceId);
        }

        public async UniTask StartCurrentInstance()
        {
            await instanceModule.StartCurrentInstance();
        }

        public async UniTask StopCurrentInstance()
        {
            await instanceModule.StopCurrentInstance();
        }
    }
}