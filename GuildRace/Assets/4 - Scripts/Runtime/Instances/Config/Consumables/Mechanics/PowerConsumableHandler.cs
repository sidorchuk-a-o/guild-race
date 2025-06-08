using AD.ToolsCollection;

namespace Game.Instances
{
    public class PowerConsumableHandler : ConsumableMechanicHandler
    {
        public int GetPower(ConsumablesItemInfo item)
        {
            return item.MechanicParams[0].IntParse();
        }

        public override void Invoke(ConsumablesItemInfo item, SquadChanceParams squadParams)
        {
            var power = GetPower(item);

            squadParams.Power += power * item.Stack.Value / item.Stack.Size;
        }
    }
}