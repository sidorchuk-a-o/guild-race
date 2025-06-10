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

        public override async UniTask Init(ItemVM itemVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(itemVM, disp, ct);

            if (ct.IsCancellationRequested) return;

            var reagentVM = itemVM as ReagentItemVM;

            rarityComponent.Init(reagentVM.RarityVM);
            stackComponent.Init(reagentVM, disp);
        }
    }
}