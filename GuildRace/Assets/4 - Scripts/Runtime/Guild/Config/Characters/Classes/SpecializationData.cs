using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Guild
{
    public class SpecializationData : ScriptableEntity
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private RoleId roleId;

        public LocalizeKey NameKey => nameKey;
        public RoleId RoleId => roleId;
    }
}