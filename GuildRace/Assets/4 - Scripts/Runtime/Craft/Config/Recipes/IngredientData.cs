﻿using System;
using UnityEngine;

namespace Game.Craft
{
    [Serializable]
    public class IngredientData
    {
        [SerializeField] private int reagentId;
        [SerializeField] private int count;

        public int ReagentId => reagentId;
        public int Count => count;
    }
}