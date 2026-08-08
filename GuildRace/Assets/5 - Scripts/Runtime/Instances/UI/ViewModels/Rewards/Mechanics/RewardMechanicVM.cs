using AD.Router;

namespace AD.Game.Instances
{
    public abstract class RewardMechanicVM : ViewModel
    {
        public int Id { get; }

        protected RewardMechanicVM(InstanceRewardData data, RewardHandler handler, InstancesVMFactory instancesVMF)
        {
            Id = data.MechanicId;
        }

        protected override void InitSubscribes()
        {
        }
    }
}