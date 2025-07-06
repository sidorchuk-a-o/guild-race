using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using VContainer;

namespace Game.Inventory
{
    public class OptionsContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private static readonly Subject<OptionsContainer> onInteracted = new();

        private readonly List<OptionButton> buttons = new();

        private UIParams config;
        private InventoryVMFactory inventoryVMF;

        public static IObservable<OptionsContainer> OnInteracted => onInteracted;

        public bool IsActive => gameObject.activeSelf;

        [Inject]
        public void Inject(InventoryConfig config, InventoryVMFactory inventoryVMF)
        {
            this.config = config.UIParams;
            this.inventoryVMF = inventoryVMF;
        }

        private void Awake()
        {
            this.SetActive(false);
        }

        public void Init()
        {
            PreloadIconAssets();
            PreloadButtonAssets();
        }

        private async void PreloadIconAssets()
        {
            var iconRefs = config.OptionHandlers
                .Select(x => x.IconRef)
                .Distinct();

            foreach (var buttonRef in iconRefs)
            {
                await inventoryVMF.PreloadIconsAsync(buttonRef);
            }
        }

        private async void PreloadButtonAssets()
        {
            var buttonRefs = config.OptionHandlers
                .Select(x => x.ButtonRef)
                .Distinct();

            foreach (var buttonRef in buttonRefs)
            {
                await inventoryVMF.PreloadObjectsAsync(buttonRef, preloadCount: 5, threshold: 1);
            }
        }

        // == Setup Container ==

        public async void SetOptions(IEnumerable<OptionKey> options)
        {
            // clear buttons
            foreach (var button in buttons)
            {
                inventoryVMF.ReturnItem(button);
            }

            buttons.Clear();

            // create buttons

            foreach (var option in options)
            {
                var optionData = config.GetOption(option);

                var buttonRef = optionData.ButtonRef;
                var buttonGO = await inventoryVMF.RentObjectAsync(buttonRef);

                var button = buttonGO.GetComponent<OptionButton>();

                buttons.Add(button);

                button.SetParent(transform);
                button.transform.localScale = Vector3.one;

                button.Init(optionData);

                buttonGO.name = $"{nameof(OptionButton)} ({optionData.Title})";
            }

            // sort
            buttons.Sort((a, b) =>
            {
                var aOrder = config.GetOptionOrder(a.Key);
                var bOrder = config.GetOptionOrder(b.Key);

                return aOrder.CompareTo(bOrder);
            });

            foreach (var button in buttons)
            {
                button.transform.SetAsLastSibling();
            }
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        // == IPointer ==

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