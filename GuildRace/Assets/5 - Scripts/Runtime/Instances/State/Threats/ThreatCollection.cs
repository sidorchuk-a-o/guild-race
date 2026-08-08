using System.Collections.Generic;

namespace AD.Game.Instances
{
    public class ThreatCollection : List<ThreatInfo>, IThreatCollection
    {
        public ThreatCollection(IEnumerable<ThreatInfo> collection) : base(collection)
        {
        }
    }
}