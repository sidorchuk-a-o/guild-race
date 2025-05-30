﻿using AD.Services.Localization;
using AD.ToolsCollection;
using Game.Inventory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Guild
{
    public class ClassData : ScriptableEntity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private LocalizeKey descKey;
        [SerializeField] private AssetReference iconRef;
        [Space]
        [SerializeField] private EquipType armorType;
        [SerializeField] private EquipType weaponType;
        [Space]
        [SerializeField] private List<SpecializationData> specs;

        private Dictionary<SpecializationId, SpecializationData> specsCache;

        public LocalizeKey NameKey => nameKey;
        public LocalizeKey DescKey => descKey;
        public AssetReference IconRef => iconRef;

        public EquipType ArmorType => armorType;
        public EquipType WeaponType => weaponType;
        public IReadOnlyList<SpecializationData> Specs => specs;

        public SpecializationData GetSpecialization(SpecializationId specId)
        {
            specsCache ??= specs.ToDictionary(x => (SpecializationId)x.Id, x => x);

            return specsCache[specId];
        }
    }
}