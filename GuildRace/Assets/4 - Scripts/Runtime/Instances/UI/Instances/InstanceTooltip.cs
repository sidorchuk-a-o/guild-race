using System.Threading;
using Cysharp.Threading.Tasks;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using UnityEngine;
using Game.UI;

namespace Game.Instances
{
    public class InstanceTooltip : TooltipContainer
    {
        [SerializeField] private UIText nameText;
        [SerializeField] private UIText descText;

        public override UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct)
        {
            var instanceVM = viewModel as InstanceVM;

            nameText.SetTextParams(instanceVM.NameKey);
            descText.SetTextParams(instanceVM.DescKey);

            return UniTask.CompletedTask;
        }
    }
}