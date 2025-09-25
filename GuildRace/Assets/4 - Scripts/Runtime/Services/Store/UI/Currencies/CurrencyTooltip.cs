using System.Threading;
using AD.Services.Router;
using AD.Services.Store;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.UI;
using UnityEngine;

namespace Game.Store
{
    public class CurrencyTooltip : TooltipContainer
    {
        [SerializeField] private UIText amountText;

        public override UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct)
        {
            var currencyVM = viewModel as CurrencyVM;
            var amountStr = currencyVM.Value.Value.ToString();

            amountText.SetTextParams(amountStr);

            return UniTask.CompletedTask;
        }
    }
}