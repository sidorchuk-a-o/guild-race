﻿using System;
using UnityEngine;

namespace Game.Inventory
{
    [Serializable]
    public class CharacterParams
    {
        [SerializeField] private float health;
        [SerializeField] private float power;
        [SerializeField] private float armor;
        [SerializeField] private float resource;

        public float Health => health;
        public float Power => power;
        public float Armor => armor;
        public float Resource => resource;
    }
}