﻿using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Guild
{
    public class GuildRankData : ScriptableEntity<string>
    {
        [SerializeField] private LocalizeKey defaultNameKey;

        public LocalizeKey DefaultNameKey => defaultNameKey;
    }
}