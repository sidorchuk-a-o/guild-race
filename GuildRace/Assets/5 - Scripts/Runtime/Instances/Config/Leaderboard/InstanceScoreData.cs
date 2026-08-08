using System;
using UnityEngine;

namespace AD.Game.Instances
{
    [Serializable]
    public class InstanceScoreData
    {
        [SerializeField] private InstanceType type;
        [SerializeField] private int score;

        public InstanceType Type => type;
        public int Score => score;
    }
}