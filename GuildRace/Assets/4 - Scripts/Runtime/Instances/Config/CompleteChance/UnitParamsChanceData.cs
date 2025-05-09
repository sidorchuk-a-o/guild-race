using System;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class UnitParamsChanceData
    {
        [SerializeField] private float totalMod;
        [SerializeField] private float charactersMod;

        public float TotalMod => totalMod;
        public float CharactersMod => charactersMod;
    }
}