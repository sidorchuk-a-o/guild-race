using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Guild
{
    [Serializable]
    public class CharactersModuleData
    {
        [SerializeField] private int maxEquipSlotCount = 6;

        [SerializeField] private List<ClassData> classes;
        [SerializeField] private List<RoleData> roles;

        public int MaxEquipSlotCount  => maxEquipSlotCount;  
        public IReadOnlyList<ClassData> Classes  => classes; 
        public IReadOnlyList<RoleData> Roles => roles;
    }
}