﻿using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Items
{
    public class EquipSlotData : ScriptableEntity
    {
        [SerializeField] private LocalizeKey nameKey;

        public LocalizeKey NameKey => nameKey;
    }
}