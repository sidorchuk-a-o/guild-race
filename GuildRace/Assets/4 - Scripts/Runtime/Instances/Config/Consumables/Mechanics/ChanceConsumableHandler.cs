using AD.ToolsCollection;

namespace Game.Instances
{
    public class ChanceConsumableHandler : ConsumableMechanicHandler
    {
        public override void Invoke(ConsumablesItemInfo item, SquadChanceParams squadParams)
        {
            var chance = item.MechanicParams[0].FloatParse();

            squadParams.ConsumableChance += chance;
        }
    }
}