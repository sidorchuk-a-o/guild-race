using System.Collections.Generic;

namespace Game.Instances
{
    public class InstancesCollection : List<InstanceInfo>, IInstancesCollection
    {
        public InstancesCollection(IEnumerable<InstanceInfo> values) : base(values)
        {
        }
    }
}