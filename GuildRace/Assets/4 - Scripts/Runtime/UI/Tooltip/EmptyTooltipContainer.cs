using System.Threading;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;

namespace Game.UI
{
    public class EmptyTooltipContainer : TooltipContainer
    {
        public override UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct)
        {
            return UniTask.CompletedTask;
        }
    }
}