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
using VContainer;

namespace Game.Inventory
{
    public class ItemSlotContainer : UIComponent<ItemSlotContainer>, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ItemSlot slot;

        [Header("Item Preview")]
        [SerializeField] private ItemPreviewContainer itemPreview;

        [Header("Pickup Process")]
        [SerializeField] private UIStates pickupStates;
        [SerializeField] private Image pickupPreviewImage;

        private static readonly Subject<ItemSlotContainer> onInited = new();
        private static readonly Subject<ItemSlotContainer> onInteracted = new();

        private readonly CompositeDisp itemDisp = new();

        private InventoryVMFactory inventoryVMF;

        private ItemInSlotComponent item;

        public static IObservable<ItemSlotContainer> OnInited => onInited;
        public static IObservable<ItemSlotContainer> OnInteracted => onInteracted;

        public ItemSlot Slot => slot;

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

            if (itemVM != null)
            {
                var itemGO = await inventoryVMF.RentItemInSlotAsync(itemVM);

                item = itemGO.GetComponent<ItemInSlotComponent>();
                item.Init(itemVM, disp);

                itemPreview.PlaceItem(item);
            }
            else if (item != null)
            {
                itemPreview.RemoveItem();
                inventoryVMF.ReturnItem(item);

                item = null;
            }
        }

        // == Pickup Preview ==

        public virtual async void ShowPickupPreview(ItemVM itemVM, string state)
        {
            if (pickupPreviewImage)
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