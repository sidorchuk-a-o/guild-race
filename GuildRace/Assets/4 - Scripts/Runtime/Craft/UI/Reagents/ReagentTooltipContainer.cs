using System.Threading;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Inventory;
using UnityEngine;

namespace Game.Craft
{
    public class ReagentTooltipContainer : ItemTooltipContainer
    {
        //[Header("Reagent")]

        public override async UniTask Init(ItemVM itemVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(itemVM, disp, ct);


        }
    }
}