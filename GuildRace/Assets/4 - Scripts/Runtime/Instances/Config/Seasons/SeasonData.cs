using AD.Services.Localization;
using AD.ToolsCollection;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class SeasonData : Entity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private List<InstanceData> instances;

        public LocalizeKey NameKey => nameKey;
        public IReadOnlyList<InstanceData> Instances => instances;
    }
}