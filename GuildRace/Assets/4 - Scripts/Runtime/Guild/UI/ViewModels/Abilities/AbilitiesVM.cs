using System.Collections.Generic;
using AD.Services.Router;
using Game.Instances;

namespace Game.Guild
{
    public class AbilitiesVM : VMCollection<AbilityData, AbilityVM>
    {
        private readonly InstancesConfig config;

        public AbilitiesVM(IReadOnlyCollection<AbilityData> values, InstancesConfig config) : base(values)
        {
            this.config = config;
        }

        protected override AbilityVM Create(AbilityData value)
        {
            return new AbilityVM(value, config);
        }
    }
}