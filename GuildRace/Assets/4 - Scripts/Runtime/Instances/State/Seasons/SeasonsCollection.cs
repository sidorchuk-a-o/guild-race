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

        public SeasonInfo GetSeason(int id)
        {
            return Values.FirstOrDefault(x => x.Id == id);
        }

        public InstanceInfo GetInstance(int id)
        {
            foreach (var season in Values)
            {
                var instance = season.GetInstanceById(id);

                if (instance != null)
                {
                    return instance;
                }
            }

            return null;
        }
    }
}