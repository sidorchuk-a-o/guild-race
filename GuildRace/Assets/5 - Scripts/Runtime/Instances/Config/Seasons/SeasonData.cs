using System;
using System.Collections.Generic;
using AD.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Instances
{
    [Serializable]
    public class SeasonData : Entity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private LocalizeKey descKey;
        [SerializeField] private List<InstanceData> instances;

        public LocalizeKey NameKey => nameKey;
        public LocalizeKey DescKey => descKey;
        public IReadOnlyList<InstanceData> Instances => instances;
    }
}