using AD.ToolsCollection;

namespace Game.Instances
{
    public class ChanceConsumableHandler : ConsumableMechanicHandler
    {
        public int GetChance(ConsumablesItemInfo item)
        {
            return item.MechanicParams[0].IntParse();
        }

        public override void Invoke(ConsumablesItemInfo item, SquadChanceParams squadParams)
        {
            var chance = GetChance(item);

            squadParams.ConsumableChance += chance;
        }
    }
}