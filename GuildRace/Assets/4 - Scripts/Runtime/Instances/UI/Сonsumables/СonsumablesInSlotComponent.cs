using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Inventory;
using UniRx;
using UnityEngine;

namespace Game.Instances
{
    public class СonsumablesInSlotComponent : ItemInSlotComponent
    {
        [Header("Stack")]
        [SerializeField] private UIStates stackableStates;
        [SerializeField] private ItemStackComponent itemStackComponent;

        public override void Init(ItemVM itemVM, CompositeDisp disp)
        {
            base.Init(itemVM, disp);

            var сonsumablesVM = itemVM as СonsumablesItemVM;

            сonsumablesVM.StackableStateVM.Value
                .Subscribe(stackableStates.SetState)
                .AddTo(disp);

            itemStackComponent.Init(сonsumablesVM.StackVM, disp);
        }
    }
}