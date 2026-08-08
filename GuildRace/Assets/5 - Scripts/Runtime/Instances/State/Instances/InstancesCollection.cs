using System.Collections.Generic;

namespace AD.Game.Instances
{
    public class InstancesCollection : List<InstanceInfo>, IInstancesCollection
    {
        public InstancesCollection(IEnumerable<InstanceInfo> values) : base(values)
        {
        }
    }
}