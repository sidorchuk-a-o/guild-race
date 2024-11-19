using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Guild
{
    public class SpecializationData : ScriptableEntity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private RoleId roleId;
        [SerializeField] private SubRoleId subRoleId;

        public LocalizeKey NameKey => nameKey;
        public RoleId RoleId => roleId;
        public SubRoleId SubRoleId => subRoleId;
    }
}