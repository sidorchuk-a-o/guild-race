using AD.States;

namespace Game.Instances
{
    public interface ISeasonsCollection : IReadOnlyReactiveCollectionInfo<SeasonInfo>
    {
        SeasonInfo GetSeason(int id);
        InstanceInfo GetInstance(int id);
    }
}