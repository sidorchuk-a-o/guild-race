using AD.ToolsCollection;
using AD.UI;
using UniRx;
using UnityEngine;

namespace Game.Inventory
{
    public class ReagentInSlotComponent : ItemInSlotComponent
    {
        [Header("Stack")]
        [SerializeField] private UIStates stackableStates;
        [SerializeField] private ItemStackComponent itemStackComponent;

        public override void Init(ItemVM itemVM, CompositeDisp disp)
        {
            base.Init(itemVM, disp);

            var materialVM = itemVM as ReagentItemVM;

            materialVM.StackableState.Value
                .Subscribe(stackableStates.SetState)
                .AddTo(disp);

            itemStackComponent.Init(materialVM.Stack, disp);
        }
    }
}