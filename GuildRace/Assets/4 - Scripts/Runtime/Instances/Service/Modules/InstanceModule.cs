using AD.Services.Router;
using Cysharp.Threading.Tasks;

namespace Game.Instances
{
    public class InstanceModule
    {
        private readonly InstancesState state;
        private readonly IRouterService router;

        public InstanceModule(InstancesState state, IRouterService router)
        {
            this.state = state;
            this.router = router;
        }

        public async UniTask StartInstance(int instanceId)
        {
            var instance = state.Seasons.GetInstance(instanceId);

            if (instance == null)
            {
                return;
            }

            // upd state
            state.SetCurrentInstance(instance);

            // start
            await StartCurrentInstance();
        }

        public async UniTask StartCurrentInstance()
        {
            if (state.HasCurrentInstance == false)
            {
                return;
            }

            await router.ShowLoading(LoadingScreenKeys.loading);

            // load map
            await InstanceLogic.GetComponent().LoadMap(state.CurrentInstance);

            // load map ui
            await router.PushAsync(
                pathKey: RouteKeys.Instances.currentInstance,
                parameters: RouteParams.FirstRoute);

            await router.HideLoading(LoadingScreenKeys.loading);
        }

        public async UniTask StopCurrentInstance()
        {
            if (state.HasCurrentInstance == false)
            {
                return;
            }

            // upd state
            state.SetCurrentInstance(null);

            await router.ShowLoading(LoadingScreenKeys.loading);

            // unload map
            await InstanceLogic.GetComponent().UnloadMap();

            // load hub ui
            await router.PushAsync(
                pathKey: RouteKeys.Hub.instances,
                parameters: RouteParams.FirstRoute);

            await router.HideLoading(LoadingScreenKeys.loading);
        }
    }
}