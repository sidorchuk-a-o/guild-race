using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Game.Instances
{
    public interface IInstancesService
    {
        ISeasonsCollection Seasons { get; }
        IActiveInstancesCollection ActiveInstances { get; }

        ActiveInstanceInfo SetupInstance { get; }

        ConsumableMechanicHandler GetMechanicHandler(int id);

        IReadOnlyCollection<SquadCandidateInfo> GetSquadCandidates();
        UniTask StartSetupInstance(SetupInstanceArgs args);
        void TryAddCharacterToSquad(string characterId);
        void TryRemoveCharacterFromSquad(string characterId);

        UniTask CompleteSetupAndStartInstance();
        void CancelSetupInstance();
        int StopActiveInstance(string activeInstanceId);
    }
}