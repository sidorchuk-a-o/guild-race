using AD.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Inventory
{
    public class EquipTypeData : ScriptableEntity<int>
    {
        [SerializeField] private LocalizeKey nameKey;

        public LocalizeKey NameKey => nameKey;
    }
}