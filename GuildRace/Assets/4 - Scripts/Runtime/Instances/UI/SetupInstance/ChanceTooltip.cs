using System.Threading;
using Cysharp.Threading.Tasks;
using AD.Services.Router;
using AD.ToolsCollection;
using Game.UI;

namespace Game.Instances
{
    public class ChanceTooltip : TooltipContainer
    {
        public override UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct)
        {
            return UniTask.CompletedTask;
        }
    }
}