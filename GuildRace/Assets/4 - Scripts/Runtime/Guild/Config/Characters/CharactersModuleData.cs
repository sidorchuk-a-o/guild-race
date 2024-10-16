using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Guild
{
    [Serializable]
    public class CharactersModuleData
    {
        [SerializeField] private int maxEquipSlotCount = 6;

        [SerializeField] private List<ClassData> classes;
        [SerializeField] private List<RoleData> roles;

        private Dictionary<RoleId, RoleData> rolesCache;
        private Dictionary<ClassId, ClassData> classesCache;
        private Dictionary<SpecializationId, SpecializationData> specsCache;
        private Dictionary<RoleId, List<(ClassData, SpecializationData)>> specByRoleCache;

        public int MaxEquipSlotCount => maxEquipSlotCount;
        public IReadOnlyList<ClassData> Classes => classes;
        public IReadOnlyList<RoleData> Roles => roles;

        public RoleData GetRole(RoleId roleId)
        {
            rolesCache ??= roles.ToDictionary(x => (RoleId)x.Id, x => x);

            return rolesCache[roleId];
        }

        public ClassData GetClass(ClassId classId)
        {
            classesCache ??= classes.ToDictionary(x => (ClassId)x.Id, x => x);

            return classesCache[classId];
        }

        public SpecializationData GetSpecialization(SpecializationId specId)
        {
            specsCache ??= classes
                .SelectMany(x => x.Specs)
                .ToDictionary(x => (SpecializationId)x.Id, x => x);

            return specsCache[specId];
        }

        public IReadOnlyList<(ClassData, SpecializationData)> GetSpecializations(RoleId roleId)
        {
            specByRoleCache ??= classes
                .SelectMany(c => c.Specs.Select(s => (classData: c, specData: s)))
                .GroupBy(x => x.specData.RoleId)
                .ToDictionary(x => x.Key, x => x.ToList());

            return specByRoleCache[roleId];
        }
    }
}