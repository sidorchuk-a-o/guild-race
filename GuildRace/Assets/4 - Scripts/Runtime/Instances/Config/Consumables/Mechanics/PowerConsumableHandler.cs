using AD.ToolsCollection;

namespace Game.Instances
{
    public class PowerConsumableHandler : ConsumableMechanicHandler
    {
        public override void Invoke(ConsumablesItemInfo item, SquadChanceParams squadParams)
        {
            var power = item.MechanicParams[0].FloatParse();

            squadParams.Power += power * item.Stack.Value / item.Stack.Size;
        }
    }
}