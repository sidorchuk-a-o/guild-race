using System.Collections.Generic;
using Game.Inventory;

namespace Game.Instances
{
    public class SquadUnitInfo
    {
        public string CharactedId { get; }

        public ItemsGridInfo Bag { get; }
        public IThreatCollection ResolvedThreats { get; }

        public SquadUnitInfo(string charactedId, ItemsGridInfo bag, IEnumerable<ThreatInfo> resolvedThreats)
        {
            CharactedId = charactedId;
            Bag = bag;
            ResolvedThreats = new ThreatCollection(resolvedThreats);
        }
    }
}