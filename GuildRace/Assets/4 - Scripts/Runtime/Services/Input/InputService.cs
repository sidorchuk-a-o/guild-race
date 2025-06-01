using AD.Services;
using AD.Services.AppEvents;
using Cysharp.Threading.Tasks;

namespace Game.Input
{
    public class InputService : Service, IInputService
    {
        private readonly InputActions actions;
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

            actions = new InputActions();

            uiModule = new UIInputModule(actions);
            inventoryModule = new InventoryInputModule(actions);
            instancesModule = new InstancesInputModule(actions);
        }

        public override async UniTask<bool> Init()
        {
            appEvents.AddAppTickListener(uiModule);
            appEvents.AddAppTickListener(inventoryModule);
            appEvents.AddAppTickListener(instancesModule);

            uiModule.Enable();
            inventoryModule.Enable();
            instancesModule.Enable();

            return await Inited();
        }

        public override void Dispose()
        {
            base.Dispose();

            appEvents.RemoveAppTickListener(uiModule);
            appEvents.RemoveAppTickListener(inventoryModule);
            appEvents.RemoveAppTickListener(instancesModule);
        }
    }
}