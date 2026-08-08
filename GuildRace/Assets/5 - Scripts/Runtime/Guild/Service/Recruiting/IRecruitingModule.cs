using System.Collections.Generic;
using R3;

namespace AD.Game.Guild
{
    public interface IRecruitingModule
    {
        ReadOnlyReactiveProperty<bool> IsEnabled { get; }

        IJoinRequestsCollection Requests { get; }
        IReadOnlyList<ClassRoleSelectorInfo> ClassRoleSelectors { get; }

        void SwitchRecruitingState();
    }
}