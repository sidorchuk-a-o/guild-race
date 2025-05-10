using System;
using UnityEngine;

namespace Game.Guild
{
    [Serializable]
    public class UnitParams
    {
        [SerializeField] private float health;
        [SerializeField] private float power;

        public float Health => health;
        public float Power => power;
    }
}