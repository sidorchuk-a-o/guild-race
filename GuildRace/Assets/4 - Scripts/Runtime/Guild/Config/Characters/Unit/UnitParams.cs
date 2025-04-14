using System;
using UnityEngine;

namespace Game.Guild
{
    [Serializable]
    public class UnitParams
    {
        [SerializeField] private float health;
        [SerializeField] private float power;
        [SerializeField] private ResourceParams resourceParams;

        public float Health => health;
        public float Power => power;
        public ResourceParams ResourceParams => resourceParams;
    }
}