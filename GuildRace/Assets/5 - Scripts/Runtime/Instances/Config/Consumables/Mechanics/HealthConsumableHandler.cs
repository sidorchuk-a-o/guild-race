using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class HealthConsumableHandler : ConsumableMechanicHandler
    {
        public static int GetHealth(ConsumablesItemData data)
        {
            return data.MechanicParams[0].IntParse();
        }

        public override void Invoke(ConsumablesItemInfo item, SquadChanceParams squadParams)
        {
            var health = item.MechanicParams[0].IntParse();

            squadParams.Health += health * item.Stack.Value / (float)item.Stack.Size;
        }
    }
}