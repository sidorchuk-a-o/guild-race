using System.Collections.Generic;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;

namespace AD.Game.Instances
{
    public interface IInstancesService
    {
        ISeasonsCollection Seasons { get; }
        IActiveInstancesCollection ActiveInstances { get; }

        ActiveInstanceInfo SetupInstance { get; }
        ActiveInstanceInfo CompletedInstance { get; }

        ReadOnlyReactiveEvent<ActiveInstanceInfo> OnInstanceCompleted { get; }
        ReadOnlyReactiveEvent<IEnumerable<RewardResult>> OnRewardsReceived { get; }

        RewardHandler GetRewardHandler(int id);
        ConsumableMechanicHandler GetConsumableHandler(int id);
        UnitCooldownInfo GetUnitCooldown(InstanceType type);

        UniTask StartSetupInstance(SetupInstanceArgs args);
        void CancelSetupInstance();

        IReadOnlyCollection<SquadCandidateInfo> GetSquadCandidates();
        void TryAddCharacterToSquad(string characterId);
        void TryRemoveCharacterFromSquad(string characterId);

        UniTask CompleteSetupAndStartInstance();
        void ForceReadyToCompleteActiveInstance(string activeInstanceId);
        int CompleteActiveInstance(string activeInstanceId);
        void ReceiveAdsRewards();

        void SetBossTimeState(bool state);
        bool HasBossComplete(int unitId);
        bool HasBossTries(int unitId);
        void AddTries(int unitId);
        float CalcChanceDiff(AddItemArgs args);
    }
}