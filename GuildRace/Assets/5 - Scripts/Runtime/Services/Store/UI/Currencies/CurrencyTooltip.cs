using System.Threading;
using AD.Game.UI;
using AD.Router;
using AD.Store;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AD.Game.Store
{
    public class CurrencyTooltip : TooltipContainer
    {
        [SerializeField] private UIText amountText;

        public override UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct)
        {
            var currencyVM = (CurrencyVM)viewModel;
            var amountStr = currencyVM.Value.Value.ToString();

            amountText.SetTextParams(amountStr);

            return UniTask.CompletedTask;
        }
    }
}