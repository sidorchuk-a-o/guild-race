using System.Collections.Generic;
using AD.Services.Router;
using Game.Instances;

namespace Game.Guild
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
            return new AbilityVM(value, instancesVMF);
        }
    }
}