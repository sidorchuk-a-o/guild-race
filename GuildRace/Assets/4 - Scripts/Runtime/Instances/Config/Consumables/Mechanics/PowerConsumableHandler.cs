using AD.ToolsCollection;

namespace Game.Instances
{
    public class PowerConsumableHandler : ConsumableMechanicHandler
    {
        public int GetPower(ConsumablesItemData data)
        {
            return data.MechanicParams[0].IntParse();
        }

        public override void Invoke(ConsumablesItemInfo item, SquadChanceParams squadParams)
        {
            var power = item.MechanicParams[0].IntParse();

            squadParams.Power += power * item.Stack.Value / item.Stack.Size;
        }
    }
}