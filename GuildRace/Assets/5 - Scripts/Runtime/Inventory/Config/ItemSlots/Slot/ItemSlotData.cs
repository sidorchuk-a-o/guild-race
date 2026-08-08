using AD.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Inventory
{
    public abstract class ItemSlotData : ScriptableEntity<int>
    {
        [SerializeField] private LocalizeKey nameKey;

        public LocalizeKey NameKey => nameKey;
    }
}