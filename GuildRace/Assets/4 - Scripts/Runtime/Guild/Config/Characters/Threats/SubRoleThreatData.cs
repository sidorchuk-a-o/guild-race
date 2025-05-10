using System;
using Game.Instances;
using UnityEngine;

namespace Game.Guild
{
    [Serializable]
    public class SubRoleThreatData
    {
        [SerializeField] private SubRoleId subRoleId;
        [SerializeField] private ThreatId threatId;

        public SubRoleId SubRoleId => subRoleId;
        public ThreatId ThreatId => threatId;
    }
}