using AD.Services.Router;

namespace Game.Instances
{
    public class ThreatsVM : VMCollection<ThreatInfo, ThreatVM>
    {
        private readonly InstancesConfig instancesConfig;

        public ThreatsVM(IThreatCollcetion values, InstancesConfig instancesConfig) : base(values)
        {
            this.instancesConfig = instancesConfig;
        }

        protected override ThreatVM Create(ThreatInfo value)
        {
            return new ThreatVM(value, instancesConfig);
        }
    }
}