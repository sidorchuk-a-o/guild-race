using AD.Services.Router;

namespace Game.Instances
{
    public abstract class RewardMechanicVM : ViewModel
    {
        public int Id { get; }

        public RewardMechanicVM(InstanceRewardData data, RewardHandler handler, InstancesVMFactory instancesVMF)
        {
            Id = data.MechanicId;
        }

        protected override void InitSubscribes()
        {
        }
    }
}