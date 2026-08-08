using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class ActiveInstancesCollection : ReactiveCollection<ActiveInstanceInfo>, IActiveInstancesCollection
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