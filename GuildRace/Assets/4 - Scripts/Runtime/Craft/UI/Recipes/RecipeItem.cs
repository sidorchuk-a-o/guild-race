using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Game.Craft
{
    public class RecipeItem : VMScrollItem<RecipeVM>
    {
        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
        }
    }
}