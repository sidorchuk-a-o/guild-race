using System;
using UnityEngine;

namespace Game.Inventory
{
    [Serializable]
    public class CharacterParams
    {
        [SerializeField] private float health;
        [SerializeField] private float power;

        public float Health => health;
        public float Power => power;
    }
}