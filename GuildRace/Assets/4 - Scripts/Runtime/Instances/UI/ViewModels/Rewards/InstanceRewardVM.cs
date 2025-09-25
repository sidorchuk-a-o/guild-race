using AD.Services.Router;

namespace Game.Instances
{
    public class InstanceRewardVM : ViewModel
    {
        public UnitVM UnitVM { get; }
        public InstanceVM InstanceVM { get; }

        public RewardMechanicVM MechanicVM { get; }
        public RewardResultVM ResultVM { get; }

        public InstanceRewardVM(InstanceRewardInfo info, InstancesVMFactory instancesVMF)
        {
            UnitVM = instancesVMF.GetBossUnit(info.Data.UnitId);
            InstanceVM = instancesVMF.GetBossInstance(info.Data.UnitId);
            MechanicVM = instancesVMF.GetRewardMechanic(info.Data);
            ResultVM = instancesVMF.GetRewardResult(info.Result);
        }

        protected override void InitSubscribes()
        {
            UnitVM.AddTo(this);
            InstanceVM.AddTo(this);
            MechanicVM.AddTo(this);
            ResultVM?.AddTo(this);
        }
    }
}