using AD.Services.Router;

namespace Game.Instances
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
            return new ThreatVM(value, instancesVMF);
        }
    }
}