using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public interface IActiveInstancesCollection : IReadOnlyReactiveCollection<ActiveInstanceInfo>
    {
        ActiveInstanceInfo GetInstance(string id);
    }
}