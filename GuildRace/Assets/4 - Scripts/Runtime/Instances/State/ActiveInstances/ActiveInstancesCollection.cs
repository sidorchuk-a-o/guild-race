using AD.States;
using System.Collections.Generic;
using System.Linq;

namespace Game.Instances
{
    public class ActiveInstancesCollection : ReactiveCollectionInfo<ActiveInstanceInfo>, IActiveInstancesCollection
    {
        public ActiveInstancesCollection(IEnumerable<ActiveInstanceInfo> values) : base(values)
        {
        }

        public ActiveInstanceInfo GetInstance(string id)
        {
            return Values.FirstOrDefault(x => x.Id == id);
        }
    }
}