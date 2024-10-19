using AD.Services;
using Cysharp.Threading.Tasks;

namespace Game.Items
{
    public class ItemsDatabaseService : Service, IItemsDatabaseService
    {
        public override async UniTask<bool> Init()
        {
            return await Inited();
        }
    }
}