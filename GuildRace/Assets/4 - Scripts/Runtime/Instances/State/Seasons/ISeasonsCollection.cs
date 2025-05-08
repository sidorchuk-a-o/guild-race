using System.Collections.Generic;

namespace Game.Instances
{
    public interface ISeasonsCollection : IReadOnlyCollection<SeasonInfo>
    {
        SeasonInfo GetSeason(int id);
        InstanceInfo GetInstance(int id);
    }
}