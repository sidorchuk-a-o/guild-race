using System;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class EquipRewardParams
    {
        [SerializeField] private InstanceType instanceType;
        [SerializeField] private int guaranteedCount;
        [SerializeField] private int chanceCount;
        [SerializeField] private float chance;

        public InstanceType InstanceType => instanceType;
        public int GuaranteedCount => guaranteedCount;
        public int ChanceCount => chanceCount;
        public float Chance => chance;
    }
}