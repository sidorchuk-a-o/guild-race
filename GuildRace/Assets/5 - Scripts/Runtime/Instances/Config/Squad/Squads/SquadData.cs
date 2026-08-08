using System;
using UnityEngine;

namespace AD.Game.Instances
{
    [Serializable]
    public class SquadData
    {
        [SerializeField] private InstanceType instanceType;
        [SerializeField] private int maxUnitsCount;

        public InstanceType InstanceType => instanceType;
        public int MaxUnitsCount => maxUnitsCount;

        public SquadData(InstanceType instanceType)
        {
            this.instanceType = instanceType;
        }
    }
}