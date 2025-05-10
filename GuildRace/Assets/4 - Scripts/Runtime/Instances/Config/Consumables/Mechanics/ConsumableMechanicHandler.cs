using AD.ToolsCollection;

namespace Game.Instances
{
    public abstract class ConsumableMechanicHandler : ScriptableEntity<int>
    {
        public abstract void Invoke(ConsumablesItemInfo item, SquadChanceParams squadParams);
    }
}