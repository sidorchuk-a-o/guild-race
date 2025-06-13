using AD.Services.Router;

namespace Game.Instances
{
    public class InstanceRewardVM : ViewModel
    {
        public RewardMechanicVM MechanicVM { get; }

        public InstanceRewardVM(InstanceRewardData data, InstancesVMFactory instancesVMF)
        {
            MechanicVM = instancesVMF.GetRewardMechanic(data);
        }

        protected override void InitSubscribes()
        {
            MechanicVM.AddTo(this);
        }
    }
}