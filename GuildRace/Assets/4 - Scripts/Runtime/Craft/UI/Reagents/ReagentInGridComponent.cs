using AD.ToolsCollection;
using AD.UI;
using Game.Inventory;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Craft
{
    public class ReagentInGridComponent : ItemInGridComponent
    {
        [Header("Stack")]
        [SerializeField] private Image rarityImage;
        [SerializeField] private UIStates stackableStates;
        [SerializeField] private ItemStackComponent itemStackComponent;

        public override void Init(ItemVM itemVM, CompositeDisp disp)
        {
            base.Init(itemVM, disp);

            var reagentVM = itemVM as ReagentItemVM;

            reagentVM.StackableStateVM.Value
                .Subscribe(stackableStates.SetState)
                .AddTo(disp);

            rarityImage.color = reagentVM.DataVM.RarityVM.Color;
            itemStackComponent.Init(reagentVM.StackVM, disp);
        }
    }
}