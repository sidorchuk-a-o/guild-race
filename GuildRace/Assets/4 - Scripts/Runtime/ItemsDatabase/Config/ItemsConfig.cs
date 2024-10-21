﻿using AD.ToolsCollection;
using UnityEngine;

namespace Game.Items
{
    public class ItemsConfig : ScriptableConfig
    {
        [SerializeField] private EquipsParams equipsParams = new();

        public EquipsParams EquipsParams => equipsParams;
    }
}