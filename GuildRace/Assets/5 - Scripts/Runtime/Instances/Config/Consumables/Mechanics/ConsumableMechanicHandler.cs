using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public abstract class ConsumableMechanicHandler : ScriptableEntity<int>
    {
        public abstract void Invoke(ConsumablesItemInfo item, SquadChanceParams squadParams);
    }
}