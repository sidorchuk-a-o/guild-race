﻿using AD.Services.Localization;
using AD.ToolsCollection;
using System;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class InstanceTypeData : Entity<int>
    {
        [SerializeField] private LocalizeKey nameKey;

        public LocalizeKey NameKey => nameKey;
    }
}