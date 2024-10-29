using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;

namespace Game.Inventory
{
    public abstract class WindowContainer : MonoBehaviour, IPointerDownHandler
    {
        [Header("Header")]
        [SerializeField] private WindowHeaderContainer headerContainer;

        private static readonly Subject<WindowContainer> onClosed = new();

        private readonly CompositeDisp windowDisp = new();

        public static IObservable<WindowContainer> OnClosed => onClosed;

        public ItemVM CurrentItem { get; private set; }
        protected InventoryVMFactory InventoryVMF { get; private set; }

        [Inject]
        public void Inject(InventoryVMFactory inventoryVMF)
        {
            InventoryVMF = inventoryVMF;
        }

        private void Awake()
        {
            headerContainer.OnClose
                .Subscribe(OnCloseCallback)
                .AddTo(this);
        }

        private void OnDisable()
        {
            windowDisp.Clear();
        }

        public async UniTask Init(string itemId, CompositeDisp disp)
        {
            windowDisp.Clear();
            windowDisp.AddTo(disp);

            CurrentItem = InventoryVMF.CreateItem(itemId);
            CurrentItem.AddTo(windowDisp);

            headerContainer.Title.SetTextParams(CurrentItem.NameKey);

            CurrentItem.IsRemoved
                .SilentSubscribe(ItemRemovedCallback)
                .AddTo(windowDisp);

            InitWindow(windowDisp);

            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);

            await UniTask.Yield();
        }

        protected abstract void InitWindow(CompositeDisp disp);

        private void ItemRemovedCallback()
        {
            OnCloseCallback();
        }

        private void OnCloseCallback()
        {
            CloseWindow();

            onClosed.OnNext(this);
        }

        public virtual void CloseWindow()
        {
        }

        // == IPointerDownHandler ==

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            transform.SetAsLastSibling();
        }
    }
}