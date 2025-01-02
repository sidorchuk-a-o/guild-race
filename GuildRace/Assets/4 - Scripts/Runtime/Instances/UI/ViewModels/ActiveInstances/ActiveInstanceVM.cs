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
        public ItemsGridVM BagVM { get; }

        public ActiveInstanceVM(
            ActiveInstanceInfo info,
            InstancesVMFactory instancesVMF,
            InventoryVMFactory inventoryVMF)
        {
            Id = info.Id;

            InstanceVM = instancesVMF.GetInstance(info.InstanceId);
            SquadVM = new IdsVM(info.Squad);
            BagVM = inventoryVMF.CreateItemsGrid(info.Bag);
        }

        protected override void InitSubscribes()
        {
            InstanceVM.AddTo(this);
            SquadVM.AddTo(this);
            BagVM.AddTo(this);
        }
    }
}