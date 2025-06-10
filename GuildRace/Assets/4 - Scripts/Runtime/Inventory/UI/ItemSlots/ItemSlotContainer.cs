using AD.UI;
using AD.Services.Router;
using AD.ToolsCollection;
using Game.UI;
using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Game.Inventory
{
    public class ItemSlotContainer : UIComponent<ItemSlotContainer>, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ItemSlot slot;
        [SerializeField] private ItemSlotsUIParams slotParams;
        [SerializeField] private ItemsTooltipUIParams tooltipParams;

        [Header("Item Preview")]
        [SerializeField] private ItemPreviewContainer itemPreview;

        [Header("Pickup Process")]
        [SerializeField] private UIStates pickupStates;
        [SerializeField] private Image pickupPreviewImage;
        [SerializeField] private string errorState = "slotError";
        [SerializeField] private string readyState = "slotReady";
        [SerializeField] private string previewState = "slotPreview";

        [Header("Params")]
        [SerializeField] private bool isReadOnly;

        private static readonly Subject<ItemSlotContainer> onInited = new();
        private static readonly Subject<ItemSlotContainer> onInteracted = new();

        private readonly CompositeDisp itemDisp = new();

        private InventoryVMFactory inventoryVMF;

        private ItemInSlotComponent item;

        public static IObservable<ItemSlotContainer> OnInited => onInited;
        public static IObservable<ItemSlotContainer> OnInteracted => onInteracted;

        public ItemSlot Slot => slot;
        public bool IsReadOnly => isReadOnly;

        public bool HasItem => ViewModel.HasItem;
        public ItemSlotVM ViewModel { get; private set; }

        [Inject]
        public void Inject(InventoryVMFactory inventoryVMF)
        {
            this.inventoryVMF = inventoryVMF;
        }

        public void Init(ItemSlotVM slotVM, CompositeDisp disp)
        {
            ViewModel = slotVM;

            if (itemPreview)
            {
                slotVM.ItemVM
                    .Subscribe(x => ItemChangedCallback(x, disp))
                    .AddTo(disp);
            }

            slotVM.PickupStateVM.Value
                .Subscribe(pickupStates.SetState)
                .AddTo(disp);

            onInited.OnNext(this);
        }

        private async void ItemChangedCallback(ItemVM itemVM, CompositeDisp disp)
        {
            itemDisp.Clear();
            itemDisp.AddTo(disp);

            var hasItem = item != null;
            var hasVM = itemVM != null;

            if (hasVM)
            {
                if (!hasItem)
                {
                    var itemParams = slotParams.GetParams(itemVM.DataVM.ItemType);
                    var itemGO = await inventoryVMF.RentObjectAsync(itemParams.ItemInSlotRef);

                    item = itemGO.GetComponent<ItemInSlotComponent>();
                }

                item.Init(itemVM, disp);
                itemPreview.PlaceItem(item);
            }
            else if (hasItem)
            {
                itemPreview.RemoveItem();
                inventoryVMF.ReturnItem(item);

                item = null;
            }
        }

        public AssetReference GetTooltipRef()
        {
            if (item == null)
            {
                return null;
            }

            var itemType = item.ViewModel.DataVM.ItemType;
            var itemParams = tooltipParams.GetParams(itemType);

            return itemParams.TooltipRef;
        }

        // == Pickup Preview ==

        public virtual async void ShowPickupPreview(ItemVM itemVM, PickupResult pickupResult)
        {
            var checkPreview = pickupResult.Context.SelectedSlotVM == ViewModel;
            var checkPlacement = ViewModel.CheckPossibilityOfPlacement(itemVM);

            var state = checkPreview
                ? previewState
                : checkPlacement
                    ? readyState
                    : errorState;

            if (pickupPreviewImage && checkPreview)
            {
                pickupPreviewImage.sprite = await itemVM.LoadIcon();
            }

            ViewModel.PickupStateVM.SetState(state);
        }

        public virtual void ResetPickupPreview()
        {
            if (pickupPreviewImage)
            {
                pickupPreviewImage.sprite = null;
            }

            ViewModel.PickupStateVM.ResetState();
        }

        // == IPointerEnter ==

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            onInteracted.OnNext(this);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            onInteracted.OnNext(null);
        }
    }
}