using System;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class UnitCooldownParams
    {
        [SerializeField] private InstanceType instanceType;
        [SerializeField] private int maxCompletedCount;
        [SerializeField] private bool isWeeklyReset;

        public InstanceType InstanceType => instanceType;
        public int MaxCompletedCount => maxCompletedCount;
        public bool IsWeeklyReset => isWeeklyReset;
    }
}