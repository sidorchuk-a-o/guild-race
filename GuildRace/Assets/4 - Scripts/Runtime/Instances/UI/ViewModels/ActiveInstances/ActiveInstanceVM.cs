using AD.Services.Router;
using Game.Inventory;
using UniRx;

namespace Game.Instances
{
    public class ActiveInstanceVM : ViewModel
    {
        public string Id { get; }

        public InstanceVM InstanceVM { get; }
        public IdsVM SquadVM { get; }

        public IReadOnlyReactiveProperty<bool> IsReadyToComplete { get; }

        public ActiveInstanceVM(
            ActiveInstanceInfo info,
            InstancesVMFactory instancesVMF,
            InventoryVMFactory inventoryVMF)
        {
            Id = info.Id;
            IsReadyToComplete = info.IsReadyToComplete;

            InstanceVM = instancesVMF.GetInstance(info.Instance);
            SquadVM = new IdsVM(info.Squad);
        }

        protected override void InitSubscribes()
        {
            InstanceVM.AddTo(this);
            SquadVM.AddTo(this);
        }
    }
}