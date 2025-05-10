using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Game.Instances
{
    public interface IInstancesService
    {
        ISeasonsCollection Seasons { get; }
        IActiveInstancesCollection ActiveInstances { get; }

        ActiveInstanceInfo SetupInstance { get; }

        RewardHandler GetRewardHandler(int id);
        ConsumableMechanicHandler GetConsumableHandler(int id);

        UniTask StartSetupInstance(SetupInstanceArgs args);
        void CancelSetupInstance();

        IReadOnlyCollection<SquadCandidateInfo> GetSquadCandidates();
        void TryAddCharacterToSquad(string characterId);
        void TryRemoveCharacterFromSquad(string characterId);

        UniTask CompleteSetupAndStartInstance();
        int CompleteActiveInstance(string activeInstanceId);
    }
}