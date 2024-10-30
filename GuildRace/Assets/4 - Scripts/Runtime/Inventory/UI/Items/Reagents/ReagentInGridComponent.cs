using AD.ToolsCollection;
using AD.UI;
using UniRx;
using UnityEngine;

namespace Game.Inventory
{
    public class ReagentInGridComponent : ItemInGridComponent
    {
        [Header("Stack")]
        [SerializeField] private UIStates stackableStates;
        [SerializeField] private ItemStackComponent itemStackComponent;

        public override void Init(ItemVM itemVM, CompositeDisp disp)
        {
            base.Init(itemVM, disp);

            var reagentVM = itemVM as ReagentItemVM;

            reagentVM.StackableStateVM.Value
                .Subscribe(stackableStates.SetState)
                .AddTo(disp);

            itemStackComponent.Init(reagentVM.StackVM, disp);
        }
    }
}