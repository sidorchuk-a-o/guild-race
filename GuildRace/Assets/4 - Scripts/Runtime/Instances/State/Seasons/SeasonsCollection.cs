using AD.States;
using System.Collections.Generic;

namespace Game.Instances
{
    public class SeasonsCollection : ReactiveCollectionInfo<SeasonInfo>, ISeasonsCollection
    {
        public SeasonsCollection(IEnumerable<SeasonInfo> values) : base(values)
        {
        }
    }
}