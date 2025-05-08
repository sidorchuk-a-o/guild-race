using Game.Inventory;

namespace Game.Instances
{
    public class SquadUnitInfo
    {
        public string CharactedId { get; }
        public ItemsGridInfo Bag { get; }

        public SquadUnitInfo(string charactedId, ItemsGridInfo bag)
        {
            CharactedId = charactedId;
            Bag = bag;
        }
    }
}