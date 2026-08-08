using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public interface ISeasonsCollection : IReadOnlyReactiveCollection<SeasonInfo>
    {
        SeasonInfo GetSeason(int id);
        InstanceInfo GetInstance(int id);
        UnitInfo GetBossUnit(int id);
    }
}