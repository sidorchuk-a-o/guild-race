using AD.ToolsCollection;

namespace Game.Instances
{
    public class HealthConsumableHandler : ConsumableMechanicHandler
    {
        public int GetHealth(ConsumablesItemInfo item)
        {
            return item.MechanicParams[0].IntParse();
        }

        public override void Invoke(ConsumablesItemInfo item, SquadChanceParams squadParams)
        {
            var health = GetHealth(item);

            squadParams.Health += health * item.Stack.Value / item.Stack.Size;
        }
    }
}