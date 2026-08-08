using System;
using AD.Game.Instances;
using AD.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Guild
{
    [Serializable]
    public class AbilityData : Entity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private LocalizeKey descKey;
        [SerializeField] private ThreatId threatId;

        public LocalizeKey NameKey => nameKey;
        public LocalizeKey DescKey => descKey;
        public ThreatId ThreatId => threatId;
    }
}