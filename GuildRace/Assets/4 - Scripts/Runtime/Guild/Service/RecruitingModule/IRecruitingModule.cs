using System.Collections.Generic;
using UniRx;

namespace Game.Guild
{
    public interface IRecruitingModule
    {
        IReadOnlyReactiveProperty<bool> IsEnabled { get; }

        IJoinRequestsCollection Requests { get; }
        IReadOnlyList<ClassWeightInfo> ClassWeights { get; }

        void SwitchRecruitingState();
    }
}