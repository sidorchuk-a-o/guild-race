using AD.ToolsCollection;

namespace Game.Instances
{
    public class ChanceConsumableHandler : ConsumableMechanicHandler
    {
        public int GetChance(ConsumablesItemData data)
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