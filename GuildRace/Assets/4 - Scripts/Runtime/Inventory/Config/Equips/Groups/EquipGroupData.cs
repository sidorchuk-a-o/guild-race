using AD.Services.Localization;
using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Inventory
{
    public class EquipGroupData : ScriptableEntity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private AssetReference iconRef;
        [SerializeField] private List<EquipTypeData> types = new();

        public LocalizeKey NameKey => nameKey;
        public AssetReference IconRef => iconRef;
        public IReadOnlyList<EquipTypeData> Types => types;
    }
}