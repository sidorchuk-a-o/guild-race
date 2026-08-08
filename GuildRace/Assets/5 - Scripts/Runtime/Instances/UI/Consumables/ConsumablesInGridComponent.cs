using AD.Game.Inventory;
using AD.ToolsCollection;
using AD.UI;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Instances
{
    public class ConsumablesInGridComponent : ItemInGridComponent
    {
        [Header("Stack")]
        [SerializeField] private Image rarityImage;
        [SerializeField] private UIStates stackableStates;
        [SerializeField] private ItemStackComponent itemStackComponent;

        public override void Init(ItemVM itemVM, CompositeDisp disp)
        {
            base.Init(itemVM, disp);

            var consumablesVM = (ConsumablesItemVM)itemVM;

            consumablesVM.StackableStateVM.Value
                .Subscribe(stackableStates.SetState)
                .AddTo(disp);

            rarityImage.color = consumablesVM.DataVM.RarityVM.Color;
            itemStackComponent.Init(consumablesVM.StackVM, disp);
        }
    }
}