using Game.Guild;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class SquadData
    {
        [SerializeField] private InstanceType instanceType;
        [SerializeField] private List<SquadRoleData> roles;
        [SerializeField] private int maxUnitsCount;

        public InstanceType InstanceType => instanceType;
        public IReadOnlyList<SquadRoleData> Roles => roles;
        public int MaxUnitsCount => maxUnitsCount;

        public SquadData(InstanceType instanceType)
        {
            this.instanceType = instanceType;
        }

        public SquadRoleData GetRole(RoleId characterRole)
        {
            return Roles.FirstOrDefault(x => x.Role == characterRole);
        }
    }
}