using System.Collections.Generic;
using AD.Services.Router;

namespace Game.Instances
{
    public class UnitsVM : VMCollection<UnitInfo, UnitVM>
    {
        private readonly InstancesVMFactory instancesVMF;

        public UnitsVM(IReadOnlyCollection<UnitInfo> values, InstancesVMFactory instancesVMF) : base(values)
        {
            this.instancesVMF = instancesVMF;
        }

        protected override UnitVM Create(UnitInfo value)
        {
            return new UnitVM(value, instancesVMF);
        }
    }
}