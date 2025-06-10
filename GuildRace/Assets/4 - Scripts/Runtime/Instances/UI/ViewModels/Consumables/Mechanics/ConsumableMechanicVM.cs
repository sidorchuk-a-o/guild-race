using AD.Services.Router;

namespace Game.Instances
{
    public abstract class ConsumableMechanicVM : ViewModel
    {
        public int Id { get; }

        public ConsumableMechanicVM(ConsumablesItemData data, ConsumableMechanicHandler handler, InstancesVMFactory instancesVMF)
        {
            Id = data.MechanicId;
        }

        protected override void InitSubscribes()
        {
        }
    }
}