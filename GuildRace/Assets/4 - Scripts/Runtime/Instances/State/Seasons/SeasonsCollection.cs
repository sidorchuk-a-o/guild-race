using AD.States;
using System.Collections.Generic;
using System.Linq;

namespace Game.Instances
{
    public class SeasonsCollection : ReactiveCollectionInfo<SeasonInfo>, ISeasonsCollection
    {
        public SeasonsCollection(IEnumerable<SeasonInfo> values) : base(values)
        {
        }

        public SeasonInfo GetById(int id)
        {
            return Values.FirstOrDefault(x => x.Id == id);
        }
    }
}