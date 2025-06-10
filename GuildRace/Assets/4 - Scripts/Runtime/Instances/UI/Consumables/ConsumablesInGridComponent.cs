using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Game.Inventory;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Instances
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

            var consumablesVM = itemVM as ConsumablesItemVM;

            consumablesVM.StackableStateVM.Value
                .Subscribe(stackableStates.SetState)
                .AddTo(disp);

            rarityImage.color = consumablesVM.RarityVM.Color;
            itemStackComponent.Init(consumablesVM.StackVM, disp);
        }
    }
}