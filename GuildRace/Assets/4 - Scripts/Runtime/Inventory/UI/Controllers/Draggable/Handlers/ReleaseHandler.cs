using AD.ToolsCollection;
using Cysharp.Threading.Tasks;

namespace Game.Inventory
{
    public abstract class ReleaseHandler : ScriptableData
    {
        public async UniTask StartProcess(ReleaseResult result)
        {
            if (CheckContext(result))
            {
                await Process(result);
            }
        }

        protected abstract bool CheckContext(ReleaseResult result);
        protected abstract UniTask Process(ReleaseResult result);
    }
}