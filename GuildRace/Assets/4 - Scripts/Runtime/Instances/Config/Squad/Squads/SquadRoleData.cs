using Game.Guild;
using System;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class SquadRoleData
    {
        [SerializeField] private RoleId role;
        [SerializeField] private int maxUnitsCount;

        public RoleId Role => role;
        public int MaxUnitsCount => maxUnitsCount;

        public SquadRoleData(RoleId role)
        {
            this.role = role;
        }
    }
}