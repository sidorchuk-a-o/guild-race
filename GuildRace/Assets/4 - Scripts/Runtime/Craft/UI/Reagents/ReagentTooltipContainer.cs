using System.Threading;
using Cysharp.Threading.Tasks;
using AD.ToolsCollection;
using Game.Inventory;
using UnityEngine;

namespace Game.Craft
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

            var dataVM = context.DataVM as ReagentDataVM;

            stackComponent.Init(context, disp);
            rarityComponent.Init(dataVM.RarityVM);
            sourceContainer.Init(dataVM, disp);
        }
    }
}