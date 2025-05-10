using AD.ToolsCollection;

namespace Game.Instances
{
    public class HealthConsumableHandler : ConsumableMechanicHandler
    {
        public override void Invoke(ConsumablesItemInfo item, SquadChanceParams squadParams)
        {
            var health = item.MechanicParams[0].FloatParse();

            squadParams.Health += health * item.Stack.Value / item.Stack.Size;
        }
    }
}