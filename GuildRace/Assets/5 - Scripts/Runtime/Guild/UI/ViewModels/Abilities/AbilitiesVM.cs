using System.Collections.Generic;
using AD.Game.Instances;
using AD.Router;

namespace AD.Game.Guild
{
    public class AbilitiesVM : VMCollection<AbilityData, AbilityVM>
    {
        private readonly InstancesVMFactory instancesVMF;

        public AbilitiesVM(IReadOnlyCollection<AbilityData> values, InstancesVMFactory instancesVMF) : base(values)
        {
            this.instancesVMF = instancesVMF;
        }

        protected override AbilityVM Create(AbilityData value)
        {
            return new(value, instancesVMF);
        }
    }
}