using AD.Services.Router;

namespace Game.Instances
{
    public class InstanceRewardVM : ViewModel
    {
        public UnitVM UnitVM { get; }
        public InstanceVM InstanceVM { get; }

        public RewardMechanicVM MechanicVM { get; }

        public InstanceRewardVM(InstanceRewardData data, InstancesVMFactory instancesVMF)
        {
            UnitVM = instancesVMF.GetBossUnit(data.UnitId);
            InstanceVM = instancesVMF.GetBossInstance(data.UnitId);
            MechanicVM = instancesVMF.GetRewardMechanic(data);
        }

        protected override void InitSubscribes()
        {
            UnitVM.AddTo(this);
            InstanceVM.AddTo(this);
            MechanicVM.AddTo(this);
        }
    }
}