using AD.ToolsCollection;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Items
{
    public class EquipSlotItem : MonoBehaviour
    {
        [Header("Preview")]
        [SerializeField] private Image iconImage;

        private CancellationTokenSource cts;

        private void Awake()
        {
            iconImage.SetActive(false);
        }

        public void Init(EquipSlotVM slotVM, CompositeDisp disp)
        {
            slotVM.ItemVM
                .Subscribe(ItemChangedCallback)
                .AddTo(disp);
        }

        private async void ItemChangedCallback(ItemVM itemVM)
        {
            var token = new CancellationTokenSource();

            cts?.Cancel();
            cts = token;

            iconImage.sprite = itemVM != null
                ? await itemVM.LoadIcon()
                : null;

            if (cts.IsCancellationRequested)
            {
                return;
            }

            iconImage.SetActive(itemVM != null);
        }
    }
}