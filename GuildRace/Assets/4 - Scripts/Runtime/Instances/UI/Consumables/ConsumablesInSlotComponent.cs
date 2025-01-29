using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Inventory;
using UniRx;
using UnityEngine;

namespace Game.Instances
{
    public class ConsumablesInSlotComponent : ItemInSlotComponent
    {
        [Header("Stack")]
        [SerializeField] private UIStates stackableStates;
        [SerializeField] private ItemStackComponent itemStackComponent;

        public override void Init(ItemVM itemVM, CompositeDisp disp)
        {
            base.Init(itemVM, disp);

            var consumablesVM = itemVM as ConsumablesItemVM;

            consumablesVM.StackableStateVM.Value
                .Subscribe(stackableStates.SetState)
                .AddTo(disp);

            itemStackComponent.Init(consumablesVM.StackVM, disp);
        }
    }
}