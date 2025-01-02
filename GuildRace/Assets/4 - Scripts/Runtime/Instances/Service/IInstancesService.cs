using Cysharp.Threading.Tasks;

namespace Game.Instances
{
    public interface IInstancesService
    {
        ISeasonsCollection Seasons { get; }
        IActiveInstancesCollection ActiveInstances { get; }

        bool HasPlayerInstance { get; }
        ActiveInstanceInfo SetupInstance { get; }
        ActiveInstanceInfo PlayerInstance { get; }

        UniTask StartSetupInstance(int instanceId);
        void TryAddCharacterToSquad(string characterId);
        void TryRemoveCharacterFromSquad(string characterId);

        UniTask CompleteSetupAndStartInstance(bool playerInstance);
        void CancelSetupInstance();

        UniTask StartPlayerInstance();
        UniTask StopPlayerInstance();
        int StopActiveInstance(string activeInstanceId);
    }
}