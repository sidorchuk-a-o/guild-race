using System;
using UnityEngine;

namespace Game.Guild
{
    [Serializable]
    public class ResourceParams
    {
        [SerializeField] private float maxValue;
        [SerializeField] private float regenValue;

        public float MaxValue => maxValue;
        public float RegenValue => regenValue;
    }
}