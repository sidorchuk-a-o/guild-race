﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Guild
{
    [Serializable]
    public class CharactersParams
    {
        [SerializeField] private List<ClassData> classes;
        [Space]
        [SerializeField] private List<RoleData> roles;
        [SerializeField] private List<SubRoleData> subRoles;
        [SerializeField] private List<ResourceData> resources;

        private Dictionary<RoleId, RoleData> rolesCache;
        private Dictionary<SubRoleId, SubRoleData> subRolesCache;
        private Dictionary<ClassId, ClassData> classesCache;
        private Dictionary<SpecializationId, SpecializationData> specsCache;
        private Dictionary<RoleId, List<(ClassData, SpecializationData)>> specByRoleCache;
        private Dictionary<ResourceId, ResourceData> resourcesCache;

        public IReadOnlyList<RoleData> Roles => roles;
        public IReadOnlyList<SubRoleData> SubRoles => subRoles;
        public IReadOnlyList<ClassData> Classes => classes;
        public IReadOnlyList<ResourceData> Resources => resources;

        public RoleData GetRole(RoleId roleId)
        {
            rolesCache ??= roles.ToDictionary(x => (RoleId)x.Id, x => x);
            rolesCache.TryGetValue(roleId, out var data);

            return data;
        }

        public RoleId GetRoleBySpec(SpecializationId specId)
        {
            var spec = GetSpecialization(specId);

            return spec.RoleId;
        }

        public SubRoleData GetSubRole(SubRoleId subRoleId)
        {
            subRolesCache ??= subRoles.ToDictionary(x => (SubRoleId)x.Id, x => x);
            subRolesCache.TryGetValue(subRoleId, out var data);

            return data;
        }

        public ClassData GetClass(ClassId classId)
        {
            classesCache ??= classes.ToDictionary(x => (ClassId)x.Id, x => x);
            classesCache.TryGetValue(classId, out var data);

            return data;
        }

        public SpecializationData GetSpecialization(SpecializationId specId)
        {
            specsCache ??= classes
                .SelectMany(x => x.Specs)
                .ToDictionary(x => (SpecializationId)x.Id, x => x);

            specsCache.TryGetValue(specId, out var data);

            return data;
        }

        public IReadOnlyList<(ClassData, SpecializationData)> GetSpecializations(RoleId roleId)
        {
            specByRoleCache ??= classes
                .SelectMany(c => c.Specs.Select(s => (classData: c, specData: s)))
                .GroupBy(x => x.specData.RoleId)
                .ToDictionary(x => x.Key, x => x.ToList());

            specByRoleCache.TryGetValue(roleId, out var data);

            return data;
        }

        public ResourceData GetResource(ResourceId resourceId)
        {
            resourcesCache ??= resources.ToDictionary(x => (ResourceId)x.Id, x => x);
            resourcesCache.TryGetValue(resourceId, out var data);

            return data;
        }
    }
}