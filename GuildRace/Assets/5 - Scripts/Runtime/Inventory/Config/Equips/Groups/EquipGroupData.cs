using System.Collections.Generic;
using AD.Localization;
using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AD.Game.Inventory
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