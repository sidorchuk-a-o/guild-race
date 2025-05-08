using System.Collections.Generic;

namespace Game.Instances
{
    public class ThreatCollection : List<ThreatInfo>, IThreatCollcetion
    {
        public ThreatCollection(IEnumerable<ThreatInfo> collection) : base(collection)
        {
        }
    }
}