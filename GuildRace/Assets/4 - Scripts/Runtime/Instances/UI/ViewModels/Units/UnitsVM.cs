using System.Collections.Generic;
using AD.Services.Router;

namespace Game.Instances
{
    public class UnitsVM : VMCollection<UnitInfo, UnitVM>
    {
        private readonly int instanceId;
        private readonly InstancesVMFactory instancesVMF;

        public UnitsVM(IReadOnlyCollection<UnitInfo> values, int instanceId, InstancesVMFactory instancesVMF) : base(values)
        {
            this.instanceId = instanceId;
            this.instancesVMF = instancesVMF;
        }

        protected override UnitVM Create(UnitInfo value)
        {
            return new UnitVM(value, instanceId, instancesVMF);
        }
    }
}