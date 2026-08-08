using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AD.Game.Instances
{
    [Serializable]
    public class CompleteChanceParams
    {
        [SerializeField] private int guaranteedCompletedCount;
        [SerializeField] private List<CompleteChanceData> parameters;

        public int GuaranteedCompletedCount => guaranteedCompletedCount;

        public CompleteChanceData GetParams(InstanceType instanceType)
        {
            return parameters.First(x => x.InstanceType == instanceType);
        }
    }
}