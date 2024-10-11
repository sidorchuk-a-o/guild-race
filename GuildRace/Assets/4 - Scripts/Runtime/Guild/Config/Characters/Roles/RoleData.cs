using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Guild
{
    public class RoleData : ScriptableEntity
    {
        [SerializeField] private LocalizeKey nameKey;

        public LocalizeKey NameKey => nameKey;
    }
}