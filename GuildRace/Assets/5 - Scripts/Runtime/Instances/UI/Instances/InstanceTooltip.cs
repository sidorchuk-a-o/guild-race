using System.Threading;
using AD.Game.UI;
using AD.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AD.Game.Instances
{
    public class InstanceTooltip : TooltipContainer
    {
        [SerializeField] private UIText nameText;
        [SerializeField] private UIText descText;

        public override UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct)
        {
            var instanceVM = (InstanceVM)viewModel;

            nameText.SetTextParams(instanceVM.NameKey);
            descText.SetTextParams(instanceVM.DescKey);

            return UniTask.CompletedTask;
        }
    }
}