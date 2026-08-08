using AD.AppEvents;
using AD.Management;
using Cysharp.Threading.Tasks;

namespace AD.Game.Input
{
    public class InputService : Service, IInputService
    {
        private readonly IAppEventsService appEvents;

        private readonly UIInputModule uiModule;
        private readonly InventoryInputModule inventoryModule;
        private readonly InstancesInputModule instancesModule;

        public IUIInputModule UIModule => uiModule;
        public IInventoryInputModule InventoryModule => inventoryModule;
        public IInstancesInputModule InstancesModule => instancesModule;

        public InputService(IAppEventsService appEvents)
        {
            this.appEvents = appEvents;

            var actions = new InputActions();

            uiModule = new(actions);
            inventoryModule = new(actions);
            instancesModule = new(actions);
        }

        public override async UniTask<bool> Init()
        {
            setupAppEvents(uiModule);
            setupAppEvents(inventoryModule);
            setupAppEvents(instancesModule);

            uiModule.Enable();
            inventoryModule.Enable();
            instancesModule.Enable();

            return await Inited();

            void setupAppEvents(InputModule module)
            {
                appEvents.AddAppTickHandler(module);
                appEvents.AddAppLateTickHandler(module);
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            removeAppEvents(uiModule);
            removeAppEvents(inventoryModule);
            removeAppEvents(instancesModule);

            return;
            void removeAppEvents(InputModule module)
            {
                appEvents.RemoveAppTickHandler(module);
                appEvents.RemoveAppLateTickHandler(module);
            }
        }
    }
}