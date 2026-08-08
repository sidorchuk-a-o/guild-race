using System;
using System.Threading;
using AD.Ads;
using AD.Game.Ads;
using AD.ProtectedTime;
using AD.Store;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer;

namespace AD.Game.Store
{
    public class AdsPurchaseButton : PurchaseButton
    {
        [Header("Ads")]
        [SerializeField] private AdsCooldownCompponent adsCooldown;
        [SerializeField] private AdsViewedCounterContainer adsViewedCounter;

        private readonly CompositeDisp activeTimerDisp = new();

        private AdsVMFactory adsVMF;
        private AdsPriceVM adsPriceVM;

        private TimerVM globalCooldownVM;
        private TimerVM activeCooldownVM;

        public override Type ViewModelType { get; } = typeof(AdsPriceVM);

        [Inject]
        public void Inject(AdsVMFactory adsVMF)
        {
            this.adsVMF = adsVMF;
        }

        public override async UniTask Init(StoreItemVM itemVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(itemVM, disp, ct);

            var adsChestVM = (AdsChestVM)itemVM;
            adsPriceVM = (AdsPriceVM)adsChestVM.PriceVM;

            globalCooldownVM = adsVMF.GetGlobalCooldown();
            globalCooldownVM.AddTo(disp);

            adsViewedCounter.Init(adsPriceVM.CounterVM, disp);

            adsPriceVM.CounterVM.IsCompleted
                .Subscribe(x => CounterCompletedCallback(x, disp))
                .AddTo(disp);
        }

        private void CounterCompletedCallback(bool isCompleted, CompositeDisp disp)
        {
            activeTimerDisp.Clear();
            activeTimerDisp.AddTo(disp);

            activeCooldownVM = isCompleted
                ? adsPriceVM.CounterVM.CooldownVM
                : globalCooldownVM;

            activeCooldownVM.TimeLeft
                .SilentSubscribe(UpdateButtonState)
                .AddTo(activeTimerDisp);

            adsCooldown.Init(activeCooldownVM, activeTimerDisp);

            UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            var timeLeft = activeCooldownVM.TimeLeft.CurrentValue;

            Button.SetInteractableState(timeLeft <= 0);
        }
    }
}