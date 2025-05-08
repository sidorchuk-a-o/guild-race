using System.Collections.Generic;
using AD.States;

namespace Game.Instances
{
    public class SquadUnitsCollection : ReactiveCollectionInfo<SquadUnitInfo>, ISquadUnitsCollection
    {
        public SquadUnitsCollection(IEnumerable<SquadUnitInfo> values) : base(values)
        {
        }
    }
}