using System.Collections.Generic;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class SquadUnitsCollection : ReactiveCollection<SquadUnitInfo>, ISquadUnitsCollection
    {
        public SquadUnitsCollection(IEnumerable<SquadUnitInfo> values) : base(values)
        {
        }
    }
}