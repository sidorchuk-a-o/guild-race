using System.Collections.Generic;

namespace Game.Instances
{
    public class ThreatCollection : List<ThreatInfo>, IThreatCollection
    {
        public ThreatCollection(IEnumerable<ThreatInfo> collection) : base(collection)
        {
        }
    }
}