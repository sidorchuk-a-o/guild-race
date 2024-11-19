using AD.Services;
using Cysharp.Threading.Tasks;

namespace Game.Instances
{
    public class InstancesService : Service, IInstancesService
    {
        public override async UniTask<bool> Init()
        {
            return await Inited();
        }
    }
}