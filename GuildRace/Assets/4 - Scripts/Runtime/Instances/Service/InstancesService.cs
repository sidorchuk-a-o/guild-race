using AD.Services;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Game.Instances
{
    public class InstancesService : Service, IInstancesService
    {
        private readonly InstancesState state;

        public ISeasonsCollection Seasons => state.Seasons;

        public InstancesService(InstancesConfig config, IObjectResolver resolver)
        {
            state = new(config, resolver);
        }

        public override async UniTask<bool> Init()
        {
            state.Init();

            return await Inited();
        }
    }
}