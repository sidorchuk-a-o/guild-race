using System;
using AD.ToolsCollection;

namespace Game.Instances
{
    [Serializable]
    public class ThreatId : Key<int>
    {
        public ThreatId()
        {
        }

        public ThreatId(int key) : base(key)
        {
        }

        public static implicit operator int(ThreatId key) => key?.value ?? -1;
        public static implicit operator ThreatId(int key) => new(key);
    }
}