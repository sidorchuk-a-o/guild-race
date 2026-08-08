using AD.ToolsCollection;
using AD.UI;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace AD.Game.Inventory
{
    public class OptionButton : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText titleText;

        [Header("Button")]
        [SerializeField] private UIButton button;

        private static readonly ReactiveEvent<OptionButton> onInteracted = new();

        private InventoryVMFactory inventoryVMF;

        public static ReadOnlyReactiveEvent<OptionButton> OnInteracted => onInteracted;

        public OptionKey Key { get; private set; }

        [Inject]
        public void Inject(InventoryVMFactory inventoryVMF)
        {
            this.inventoryVMF = inventoryVMF;
        }

        private void Awake()
        {
            button.OnClick
                .Subscribe(OnClickCallback)
                .AddTo(this);
        }

        public async void Init(OptionHandler option)
        {
            Key = option.Key;

            titleText.SetTextParams(option.TitleKey);
            iconImage.sprite = await inventoryVMF.RentImage(option.IconRef);
        }

        private void OnClickCallback()
        {
            onInteracted.OnNext(this);
        }
    }
}