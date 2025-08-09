using System.Threading;
using AD.Services.Store;
using AD.ToolsCollection;
using Game.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Store
{
    public class StoreItemItem : MonoBehaviour
    {
        [Header("Store Item")]
        [SerializeField] private Image iconImage;
        [SerializeField] private TooltipComponent tooltipComponent;
        [SerializeField] private PurchaseButtonsContainer purchaseButtons;

        public async void Init(StoreItemVM itemVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            var icon = await itemVM.LoadIcon(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = icon;
            tooltipComponent.Init(itemVM);

            await purchaseButtons.Init(itemVM, disp, ct);
        }
    }
}