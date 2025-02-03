using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Guild
{
    public class SpecializationData : ScriptableEntity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private LocalizeKey descKey;
        [Space]
        [SerializeField] private RoleId roleId;
        [SerializeField] private SubRoleId subRoleId;
        [Space]
        [SerializeField] private UnitParams unitParams;

        public LocalizeKey NameKey => nameKey;
        public LocalizeKey DescKey => descKey;

        public RoleId RoleId => roleId;
        public SubRoleId SubRoleId => subRoleId;

        public UnitParams UnitParams => unitParams;
    }
}