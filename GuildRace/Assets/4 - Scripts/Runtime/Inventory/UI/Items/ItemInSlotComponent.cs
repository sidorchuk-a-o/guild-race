using AD.UI;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public abstract class ItemInSlotComponent : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private Image iconImage;

        [Header("States")]
        [SerializeField] private UIStates highlightStates;

        public ItemVM ViewModel { get; private set; }

        public virtual async void Init(ItemVM itemVM, CompositeDisp disp)
        {
            ViewModel = itemVM;

            iconImage.sprite = await itemVM.LoadIcon();

            itemVM.HighlightStateVM.Value
                .Subscribe(highlightStates.SetState)
                .AddTo(disp);
        }
    }
}