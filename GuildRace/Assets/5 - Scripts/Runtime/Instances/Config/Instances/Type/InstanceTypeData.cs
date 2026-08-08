using System;
using AD.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Instances
{
    [Serializable]
    public class InstanceTypeData : Entity<int>
    {
        [SerializeField] private LocalizeKey nameKey;

        public InstanceType Type => Id;
        public LocalizeKey NameKey => nameKey;
    }
}