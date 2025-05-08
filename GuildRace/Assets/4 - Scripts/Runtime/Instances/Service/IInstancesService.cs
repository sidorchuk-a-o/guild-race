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

        UniTask StartSetupInstance(SetupInstanceArgs args);
        void CancelSetupInstance();
        
        IReadOnlyCollection<SquadCandidateInfo> GetSquadCandidates();
        void TryAddCharacterToSquad(string characterId);
        void TryRemoveCharacterFromSquad(string characterId);
        
        UniTask CompleteSetupAndStartInstance();
        int StopActiveInstance(string activeInstanceId);
    }
}