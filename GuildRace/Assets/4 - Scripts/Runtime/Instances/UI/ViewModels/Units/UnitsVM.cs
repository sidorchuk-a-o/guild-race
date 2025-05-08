using System.Collections.Generic;
using AD.Services.Router;

namespace Game.Instances
{
    public class UnitsVM : VMCollection<UnitInfo, UnitVM>
    {
        private readonly InstancesConfig config;

        public UnitsVM(IReadOnlyCollection<UnitInfo> values, InstancesConfig config) : base(values)
        {
            this.config = config;
        }

        protected override UnitVM Create(UnitInfo value)
        {
            return new UnitVM(value, config);
        }
    }
}