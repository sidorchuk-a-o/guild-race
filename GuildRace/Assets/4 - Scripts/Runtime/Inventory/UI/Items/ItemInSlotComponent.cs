using AD.UI;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Game.Inventory
{
    public abstract class ItemInSlotComponent : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private ItemIconImage iconImage;

        [Header("States")]
        [SerializeField] private UIStates highlightStates;

        public ItemVM ViewModel { get; private set; }

        public virtual void Init(ItemVM itemVM, CompositeDisp disp)
        {
            ViewModel = itemVM;

            iconImage.UpdateIcon(itemVM, iconStaticOn: true, disp);

            itemVM.HighlightStateVM.Value
                .Subscribe(highlightStates.SetState)
                .AddTo(disp);
        }
    }
}