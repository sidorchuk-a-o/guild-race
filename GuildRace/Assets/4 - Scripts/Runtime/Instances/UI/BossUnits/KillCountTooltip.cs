using System.Threading;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.UI;

namespace Game.Instances
{
    public class KillCountTooltip : TooltipContainer
    {
        public override UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct)
        {
            return UniTask.CompletedTask;
        }
    }
}