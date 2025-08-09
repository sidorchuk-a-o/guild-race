using System;
using UnityEngine;

namespace Game.Store
{
    [Serializable]
    public class ReagentRewardData
    {
        [SerializeField] private int reagentId;
        [SerializeField] private int amount;

        public int ReagentId => reagentId;
        public int Amount => amount;
    }
}