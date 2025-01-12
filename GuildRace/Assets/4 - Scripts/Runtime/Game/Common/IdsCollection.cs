using AD.States;
using System.Collections.Generic;

namespace Game.Instances
{
    public class IdsCollection : ReactiveCollectionInfo<string>, IIdsCollection
    {
        public IdsCollection(IEnumerable<string> values = null) : base(values)
        {
        }
    }
}