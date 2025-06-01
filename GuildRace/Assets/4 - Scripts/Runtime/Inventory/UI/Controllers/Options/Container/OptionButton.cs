using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;
using UnityEngine.UI;
using AD.UI;
using System;

namespace Game.Inventory
{
    public class OptionButton : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText titleText;

        [Header("Button")]
        [SerializeField] private UIButton button;

        private static readonly Subject<OptionButton> onInteracted = new();

        private InventoryVMFactory inventoryVMF;

        public static IObservable<OptionButton> OnInteracted => onInteracted;

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