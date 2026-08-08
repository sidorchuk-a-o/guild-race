using AD.Router;

namespace AD.Game.Instances
{
    public abstract class ConsumableMechanicVM : ViewModel
    {
        public int Id { get; }

        protected ConsumableMechanicVM(ConsumablesItemData data, ConsumableMechanicHandler handler, InstancesVMFactory instancesVMF)
        {
            Id = data.MechanicId;
        }

        protected override void InitSubscribes()
        {
        }
    }
}