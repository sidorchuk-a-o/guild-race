using AD.States;

namespace Game.Instances
{
    public interface ISeasonsCollection : IReadOnlyReactiveCollectionInfo<SeasonInfo>
    {
        SeasonInfo GetById(int id);
    }
}