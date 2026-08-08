using System.Threading;
using AD.Game.UI;
using AD.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;

namespace AD.Game.Guild
{
    public class SubRoleTooltip : TooltipContainer
    {
        public override UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct)
        {
            return UniTask.CompletedTask;
        }
    }
}