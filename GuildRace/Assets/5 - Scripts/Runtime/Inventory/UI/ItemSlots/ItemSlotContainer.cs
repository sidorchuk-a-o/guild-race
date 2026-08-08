using AD.Audio;
using AD.Game.UI;
using AD.ToolsCollection;
using AD.UI;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;

namespace AD.Game.Inventory
{
    public class ItemSlotContainer : UIComponent<ItemSlotContainer>, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ItemSlot slot;
        [SerializeField] private ItemSlotsUIParams slotParams;
        [SerializeField] private ItemsTooltipUIParams tooltipParams;

        [Header("Item Preview")]
        [SerializeField] private ItemPreviewContainer itemPreview;
        [SerializeField] private AudioKey addSfxKey;

        [Header("Pickup Process")]
        [SerializeField] private UIStates pickupStates;
        [SerializeField] private Image pickupPreviewImage;
        [SerializeField] private string errorState = "slotError";
        [SerializeField] private string readyState = "slotReady";
        [SerializeField] private string previewState = "slotPreview";

        [Header("Params")]
        [SerializeField] private bool isReadOnly;

        private static readonly ReactiveEvent<ItemSlotContainer> onInited = new();
        private static readonly ReactiveEvent<ItemSlotContainer> onInteracted = new();

        private readonly CompositeDisp itemDisp = new();

        private IAudioService audioService;
        private InventoryVMFactory inventoryVMF;

        private ItemInSlotComponent item;

        public static ReadOnlyReactiveEvent<ItemSlotContainer> OnInited => onInited;
        public static ReadOnlyReactiveEvent<ItemSlotContainer> OnInteracted => onInteracted;

        public ItemSlot Slot => slot;
        public bool IsReadOnly => isReadOnly;

        public bool HasItem => ViewModel.HasItem;
        public ItemSlotVM ViewModel { get; private set; }

        [Inject]
        public void Inject(IAudioService audioService, InventoryVMFactory inventoryVMF)
        {
            this.audioService = audioService;
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

            slotVM.ItemVM
                .SilentSubscribe(PlaySfxEffect)
                .AddTo(disp);

            slotVM.PickupStateVM.Value
                .Subscribe(pickupStates.SetState)
                .AddTo(disp);

            onInited.OnNext(this);
        }

        private void PlaySfxEffect(ItemVM itemVM)
        {
            if (itemVM != null)
            {
                audioService?.UiModule.TryPlaySound(addSfxKey);
            }
        }

        private async void ItemChangedCallback(ItemVM itemVM, CompositeDisp disp)
        {
            itemDisp.Clear();
            itemDisp.AddTo(disp);

            var hasItem = item is not null;
            var hasVM = itemVM is not null;

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
            if (item is null)
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