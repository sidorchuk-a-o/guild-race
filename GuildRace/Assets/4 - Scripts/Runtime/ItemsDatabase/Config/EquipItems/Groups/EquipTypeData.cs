using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Items
{
    public class EquipTypeData : ScriptableEntity
    {
        [SerializeField] private LocalizeKey nameKey;

        public LocalizeKey NameKey => nameKey;
    }
}