using AD.Services.Router;

namespace Game.Instances
{
    public abstract class ConsumableMechanicVM : ViewModel
    {
        private readonly ConsumablesItemInfo info;
        private readonly ConsumableMechanicHandler handler;

        public int Id { get; }

        public ConsumableMechanicVM(ConsumablesItemInfo info, ConsumableMechanicHandler handler, InstancesVMFactory instancesVMF)
        {
            this.info = info;
            this.handler = handler;

            Id = info.MechanicId;
        }

        protected override void InitSubscribes()
        {
        }
    }
}