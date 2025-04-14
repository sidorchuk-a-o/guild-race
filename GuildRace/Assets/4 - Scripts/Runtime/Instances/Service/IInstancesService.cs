using Cysharp.Threading.Tasks;

namespace Game.Instances
{
    public interface IInstancesService
    {
        ISeasonsCollection Seasons { get; }
        IActiveInstancesCollection ActiveInstances { get; }

        ActiveInstanceInfo SetupInstance { get; }

        UniTask StartSetupInstance(int instanceId);
        void TryAddCharacterToSquad(string characterId);
        void TryRemoveCharacterFromSquad(string characterId);

        UniTask CompleteSetupAndStartInstance();
        void CancelSetupInstance();
        int StopActiveInstance(string activeInstanceId);
    }
}