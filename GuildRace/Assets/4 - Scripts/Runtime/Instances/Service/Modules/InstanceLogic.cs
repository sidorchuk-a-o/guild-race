using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Components;
using VContainer;
using VContainer.Unity;

namespace Game.Instances
{
    public class InstanceLogic : GameComponent<InstanceLogic>
    {
        private IObjectResolver resolver;

        private InstanceMapComponent instanceMap;
        private AddressableGameObject instanceMapRef;

        [Inject]
        public void Inject(IObjectResolver resolver)
        {
            this.resolver = resolver;
        }

        // == Map ==

        public async UniTask LoadMap(InstanceInfo instance)
        {
            instanceMapRef = instance.MapRef;
            instanceMapRef.SetParent(gameObject);

            var instanceMapGO = await instanceMapRef.LoadAsync();

            resolver.InjectGameObject(instanceMapGO);

            instanceMap = instanceMapGO.GetComponent<InstanceMapComponent>();
            instanceMap.Init();
        }

        public async UniTask UnloadMap()
        {
            await instanceMapRef.ReleaseAsync();

            instanceMapRef = null;
            instanceMap = null;
        }
    }
}