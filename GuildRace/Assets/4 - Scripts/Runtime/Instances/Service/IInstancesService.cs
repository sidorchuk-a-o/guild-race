using Cysharp.Threading.Tasks;

namespace Game.Instances
{
    public interface IInstancesService
    {
        ISeasonsCollection Seasons { get; }

        bool HasCurrentInstance { get; }
        InstanceInfo CurrentInstance { get; }

        UniTask StartInstance(int instanceId);
        UniTask StartCurrentInstance();
        UniTask StopCurrentInstance();
    }
}