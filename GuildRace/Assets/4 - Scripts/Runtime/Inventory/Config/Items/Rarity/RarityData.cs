﻿using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Inventory
{
    public class RarityData : ScriptableEntity<int>
    {
        [SerializeField] private LocalizeKey nameKey;

        public LocalizeKey NameKey => nameKey;
    }
}