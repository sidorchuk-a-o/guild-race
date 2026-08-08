using R3;

namespace AD.Game.Guild
{
    public class ClassRoleSelectorInfo
    {
        private readonly ReactiveProperty<bool> isEnabled = new();

        public RoleId RoleId { get; }
        public ReadOnlyReactiveProperty<bool> IsEnabled => isEnabled;

        public ClassRoleSelectorInfo(RoleId roleId, bool isEnabled)
        {
            RoleId = roleId;

            this.isEnabled.Value = isEnabled;
        }

        public void SetActiveState(bool value)
        {
            isEnabled.Value = value;
        }
    }
}