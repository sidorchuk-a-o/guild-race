using AD.States;
using System.Collections.Generic;

namespace Game.Instances
{
    public class InstancesCollection : ReactiveCollectionInfo<InstanceInfo>, IInstancesCollection
    {
        public InstancesCollection(IEnumerable<InstanceInfo> values) : base(values)
        {
        }
    }
}