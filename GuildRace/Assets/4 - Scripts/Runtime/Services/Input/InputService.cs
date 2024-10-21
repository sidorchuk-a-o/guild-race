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

        public IInventoryInputModule InventoryModule => inventoryModule;

        public InputService(IAppEventsService appEvents)
        {
            this.appEvents = appEvents;

            actions = new InputActions();

            inventoryModule = new InventoryInputModule(actions);
        }

        public override async UniTask<bool> Init()
        {
            appEvents.AddAppTickListener(inventoryModule);

            return await Inited();
        }

        public override void Dispose()
        {
            base.Dispose();

            appEvents.RemoveAppTickListener(inventoryModule);
        }
    }
}