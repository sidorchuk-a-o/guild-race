﻿using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Inventory
{
    public abstract class ItemSlotData : ScriptableEntity
    {
        [SerializeField] private LocalizeKey nameKey;

        public LocalizeKey NameKey => nameKey;
    }
}