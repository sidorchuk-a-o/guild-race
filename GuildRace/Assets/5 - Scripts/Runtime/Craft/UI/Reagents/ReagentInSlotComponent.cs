using AD.Game.Inventory;
using AD.ToolsCollection;
using AD.UI;
using R3;
using UnityEngine;

namespace AD.Game.Craft
{
    public class ReagentInSlotComponent : ItemInSlotComponent
    {
        [Header("Stack")]
        [SerializeField] private UIStates stackableStates;
        [SerializeField] private ItemStackComponent itemStackComponent;

        public override void Init(ItemVM itemVM, CompositeDisp disp)
        {
            base.Init(itemVM, disp);

            var reagentVM = (ReagentItemVM)itemVM;

            reagentVM.StackableStateVM.Value
                .Subscribe(stackableStates.SetState)
                .AddTo(disp);

            itemStackComponent.Init(reagentVM.StackVM, disp);
        }
    }
}