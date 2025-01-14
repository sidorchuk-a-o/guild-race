using AD.Services;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Game.Craft
{
    public class CraftService : Service, ICraftService
    {
        private readonly CraftState state;

        public CraftService(CraftConfig config, IObjectResolver resolver)
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