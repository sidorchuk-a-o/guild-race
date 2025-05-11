using System;
using System.Collections.Generic;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class InstanceRewardData : Entity<int>
    {
        [SerializeField] private int unitId;
        [SerializeField] private int mechanicId;
        [SerializeField] private List<string> mechanicParams;

        public int UnitId => unitId;
        public int MechanicId => mechanicId;
        public IReadOnlyList<string> MechanicParams => mechanicParams;
    }
}