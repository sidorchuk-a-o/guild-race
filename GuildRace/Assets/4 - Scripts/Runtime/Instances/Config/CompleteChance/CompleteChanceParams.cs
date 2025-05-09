using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class CompleteChanceParams
    {
        [SerializeField] private List<CompleteChanceData> parameters;

        public CompleteChanceData GetParams(InstanceType instanceType)
        {
            return parameters.First(x => x.InstanceType == instanceType);
        }
    }
}