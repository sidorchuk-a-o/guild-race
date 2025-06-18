using AD.ToolsCollection;

namespace Game.Instances
{
    public class HealthConsumableHandler : ConsumableMechanicHandler
    {
        public int GetHealth(ConsumablesItemData data)
        {
            return data.MechanicParams[0].IntParse();
        }

        public override void Invoke(ConsumablesItemInfo item, SquadChanceParams squadParams)
        {
            var health = item.MechanicParams[0].IntParse();

            squadParams.Health += health * item.Stack.Value / item.Stack.Size;
        }
    }
}