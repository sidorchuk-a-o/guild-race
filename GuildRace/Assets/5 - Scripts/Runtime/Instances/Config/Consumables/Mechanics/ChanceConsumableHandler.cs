using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class ChanceConsumableHandler : ConsumableMechanicHandler
    {
        public static int GetChance(ConsumablesItemData data)
        {
            return data.MechanicParams[0].IntParse();
        }

        public override void Invoke(ConsumablesItemInfo item, SquadChanceParams squadParams)
        {
            var chance = item.MechanicParams[0].IntParse();

            squadParams.ConsumableChance += chance;
        }
    }
}