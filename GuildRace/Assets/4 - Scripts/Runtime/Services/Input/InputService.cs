using AD.Services;
using AD.Services.AppEvents;
using Cysharp.Threading.Tasks;

namespace Game.Input
{
    public class InputService : Service, IInputService
    {
        private readonly InputActions actions;
        private readonly IAppEventsService appEvents;

        private readonly InventoryInputModule inventoryModule;
        private readonly MapInputModule mapModule;

        public IInventoryInputModule InventoryModule => inventoryModule;
        public IMapInputModule MapModule => mapModule;

        public InputService(IAppEventsService appEvents)
        {
            this.appEvents = appEvents;

            actions = new InputActions();

            inventoryModule = new InventoryInputModule(actions);
            mapModule = new MapInputModule(actions);
        }

        public override async UniTask<bool> Init()
        {
            appEvents.AddAppTickListener(inventoryModule);
            appEvents.AddAppTickListener(mapModule);

            inventoryModule.Enable();
            mapModule.Enable();

            return await Inited();
        }

        public override void Dispose()
        {
            base.Dispose();

            appEvents.RemoveAppTickListener(inventoryModule);
        }
    }
}