using System;
using AD.Services.Localization;
using AD.ToolsCollection;
using Game.Instances;
using UnityEngine;

namespace Game.Guild
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