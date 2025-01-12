using AD.Services.Router;
using Game.Guild;
using UniRx;

namespace Game.Instances
{
    public class SquadRolesCounterVM : ViewModel
    {
        private readonly SquadRoleData roleData;
        private readonly ReactiveProperty<string> countStr = new();

        private int count;

        public RoleVM RoleVM { get; }
        public IReadOnlyReactiveProperty<string> CountStr => countStr;

        public SquadRolesCounterVM(SquadRoleData roleData, GuildVMFactory guildVMF)
        {
            this.roleData = roleData;

            RoleVM = guildVMF.GetRole(roleData.Role);

            SetCount(count);
        }

        protected override void InitSubscribes()
        {
            RoleVM.AddTo(this);
        }

        public void IncreaseCount()
        {
            count++;

            SetCount(count);
        }

        public void DecreaseCount()
        {
            count--;

            SetCount(count);
        }

        private void SetCount(int count)
        {
            countStr.Value = $"{count} / {roleData.MaxUnitsCount}";
        }
    }
}