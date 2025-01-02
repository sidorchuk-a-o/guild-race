using System;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class SquadContainerParams
    {
        [SerializeField] private InstanceType type;
        [SerializeField] private SquadUnitsContainer squadPrefab;

        public InstanceType Type => type;
        public SquadUnitsContainer SquadPrefab => squadPrefab;
    }
}