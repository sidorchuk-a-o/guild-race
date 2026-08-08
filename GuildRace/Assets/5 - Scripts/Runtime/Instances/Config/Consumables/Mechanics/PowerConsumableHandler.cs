using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class PowerConsumableHandler : ConsumableMechanicHandler
    {
        public static int GetPower(ConsumablesItemData data)
        {
            return data.MechanicParams[0].IntParse();
        }

        public override void Invoke(ConsumablesItemInfo item, SquadChanceParams squadParams)
        {
            var power = item.MechanicParams[0].IntParse();

            squadParams.Power += power * item.Stack.Value / (float)item.Stack.Size;
        }
    }
}