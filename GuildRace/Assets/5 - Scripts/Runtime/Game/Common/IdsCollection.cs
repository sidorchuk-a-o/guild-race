using System.Collections.Generic;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class IdsCollection : ReactiveCollection<string>, IIdsCollection
    {
        public IdsCollection(IEnumerable<string> values = null) : base(values)
        {
        }
    }
}