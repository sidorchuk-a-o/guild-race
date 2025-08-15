using AD.UI;
using AD.Services.Router;
using AD.Services.Store;
using AD.ToolsCollection;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Store
{
    public class StoreContainer : UIContainer
    {
        [Header("Shelves")]
        [SerializeField] private StoreShelvesScrollView storeShelvesScroll;

        [Header("Selected Shelf")]
        [SerializeField] private CanvasGroup shelfContainer;
        [SerializeField] private StoreItemsGrid storeItemsGrid;

        private readonly CompositeDisp shelfDisp = new();
        private CancellationTokenSource shelfToken;

        private StoreShelvesVM storeShelvesVM;

        private string lastShelfId;
        private StoreShelfVM shelfVM;

        [Inject]
        public void Inject(StoreVMFactory storeVMF)
        {
            storeShelvesVM = storeVMF.GetShelves();
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            // shelves
            storeShelvesVM.AddTo(disp);

            storeShelvesScroll.Init(storeShelvesVM, true);

            storeShelvesScroll.OnSelect
                .Subscribe(SelectShelf)
                .AddTo(disp);

            SelectShelf(shelfVM ?? storeShelvesVM.FirstOrDefault());
        }

        private void SelectShelf(StoreShelfVM shelfVM)
        {
            this.shelfVM?.SetSelectState(false);

            this.shelfVM = shelfVM;
            this.shelfVM?.SetSelectState(true);

            UpdateShelfBlock();
        }

        private async void UpdateShelfBlock()
        {
            shelfDisp.Clear();
            shelfDisp.AddTo(disp);

            var token = new CancellationTokenSource();

            shelfToken?.Cancel();
            shelfToken = token;

            shelfVM?.SetSelectState(false);

            if (lastShelfId.IsValid() &&
                lastShelfId == shelfVM.Id)
            {
                updateShelf();
                return;
            }

            lastShelfId = shelfVM.Id;

            shelfContainer.DOKill();
            shelfContainer.SetInteractable(true);

            const float duration = 0.1f;

            await shelfContainer.DOFade(0, duration);

            if (token.IsCancellationRequested)
            {
                return;
            }

            updateShelf();

            await shelfContainer.DOFade(1, duration);

            void updateShelf()
            {
                shelfVM?.SetSelectState(true);

                storeItemsGrid.Init(shelfVM.StoreItemsVM, shelfDisp, shelfToken);
            }
        }
    }
}