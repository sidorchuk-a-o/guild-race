using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Guild
{
    public class SpecializationData : ScriptableEntity<int>
    {
        [SerializeField] private RoleId roleId;
        [SerializeField] private SubRoleId subRoleId;
        [SerializeField] private LocalizeKey nameKey;

        public RoleId RoleId => roleId;
        public SubRoleId SubRoleId => subRoleId;
        public LocalizeKey NameKey => nameKey;
    }
}