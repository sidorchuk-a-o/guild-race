using System.Threading;
using AD.Game.Inventory;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AD.Game.Craft
{
    public class ReagentTooltipContainer : ItemTooltipContainer
    {
        [Header("Reagent")]
        [SerializeField] private RarityComponent rarityComponent;
        [SerializeField] private TooltipItemStackComponent stackComponent;
        [SerializeField] private ReagentSourceContainer sourceContainer;

        public override async UniTask Init(TooltipContext context, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(context, disp, ct);

            if (ct.IsCancellationRequested) return;

            var dataVM = (ReagentDataVM)context.DataVM;

            stackComponent.Init(context, disp);
            rarityComponent.Init(dataVM.RarityVM);
            sourceContainer.Init(dataVM, disp);
        }
    }
}