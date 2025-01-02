using AD.States;

namespace Game.Instances
{
    public interface IActiveInstancesCollection : IReadOnlyReactiveCollectionInfo<ActiveInstanceInfo>
    {
        ActiveInstanceInfo GetInstance(string id);
    }
}