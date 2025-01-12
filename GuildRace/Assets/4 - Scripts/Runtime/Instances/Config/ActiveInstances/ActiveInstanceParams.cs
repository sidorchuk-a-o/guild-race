using System;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class ActiveInstanceParams
    {
        [SerializeField] private int tempCompeteTime = 30;

        public int TempCompeteTime => tempCompeteTime;
    }
}