using AD.Services.Localization;
using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    public class EquipGroupData : ScriptableEntity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private List<EquipTypeData> types = new();

        public LocalizeKey NameKey => nameKey;
        public IReadOnlyList<EquipTypeData> Types => types;
    }
}