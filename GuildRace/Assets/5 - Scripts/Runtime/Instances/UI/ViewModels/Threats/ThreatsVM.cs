using AD.Router;

namespace AD.Game.Instances
{
    public class ThreatsVM : VMCollection<ThreatInfo, ThreatVM>
    {
        private readonly InstancesVMFactory instancesVMF;

        public ThreatsVM(IThreatCollection values, InstancesVMFactory instancesVMF) : base(values)
        {
            this.instancesVMF = instancesVMF;
        }

        protected override ThreatVM Create(ThreatInfo value)
        {
            return new(value, instancesVMF);
        }
    }
}