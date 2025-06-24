using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Game.Instances
{
    public interface IInstancesService
    {
        ISeasonsCollection Seasons { get; }
        IActiveInstancesCollection ActiveInstances { get; }

        ActiveInstanceInfo SetupInstance { get; }
        ActiveInstanceInfo CompletedInstance { get; }

        IObservable<ActiveInstanceInfo> OnInstanceCompleted { get; }
        IObservable<IEnumerable<RewardResult>> OnRewardsReceived { get; }

        RewardHandler GetRewardHandler(int id);
        ConsumableMechanicHandler GetConsumableHandler(int id);

        UniTask StartSetupInstance(SetupInstanceArgs args);
        void CancelSetupInstance();

        IReadOnlyCollection<SquadCandidateInfo> GetSquadCandidates();
        void TryAddCharacterToSquad(string characterId);
        void TryRemoveCharacterFromSquad(string characterId);

        UniTask CompleteSetupAndStartInstance();
        int CompleteActiveInstance(string activeInstanceId);

        void SetBossTimeState(bool state);
        bool CheckUnitCooldown(int unitId, int instanceId);
        float CalcChanceDiff(AddItemArgs args);
    }
}