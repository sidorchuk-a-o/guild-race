using System.Threading;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.UI;

namespace Game.Leaderboards
{
    public class ScoreTooltip : TooltipContainer
    {
        public override UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct)
        {
            return UniTask.CompletedTask;
        }
    }
}