using System.Collections.Generic;

namespace Game.Guild
{
    public interface IRecruitingModule
    {
        IJoinRequestsCollection Requests { get; }
        IReadOnlyList<ClassWeightInfo> ClassWeights { get; }
    }
}