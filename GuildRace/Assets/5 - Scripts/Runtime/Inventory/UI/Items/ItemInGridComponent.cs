using AD.Game.UI;
using AD.ToolsCollection;
using AD.UI;
using R3;
using UnityEngine;

namespace AD.Game.Inventory
{
    public abstract class ItemInGridComponent : UIComponent<ItemInGridComponent>
    {
        [Header("Params")]
        [SerializeField] private ItemIconImage iconImage;

        [Header("States")]
        [SerializeField] private UIStates highlightStates;

        public ItemVM ViewModel { get; private set; }

        public virtual void Init(ItemVM itemVM, CompositeDisp disp)
        {
            ViewModel = itemVM;

            iconImage.UpdateIcon(itemVM, iconStaticOn: false, disp);

            itemVM.HighlightStateVM.Value
                .Subscribe(highlightStates.SetState)
                .AddTo(disp);
        }
    }
}