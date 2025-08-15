using AD.Services.Ads;
using AD.Services.ProtectedTime;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Ads
{
    public class AdsButton : MonoBehaviour
    {
        [SerializeField] private AdsButtonKey key;
        [Space]
        [SerializeField] private UIButton button;
        [SerializeField] private GameObject loadingIndicator;
        [Space]
        [SerializeField] private AdsCooldownCompponent adsCooldown;
        [SerializeField] private AdsViewedCounterContainer adsViewedCounter;

        private readonly CompositeDisp activeTimerDisp = new();
        private readonly Subject onRewarded = new();

        private IAdsService ads;
        private AdsVMFactory adsVMF;

        private bool inProcess;
        private AdsButtonVM buttonVM;

        private TimerVM globalCooldownVM;
        private TimerVM activeCooldownVM;

        public IObservable OnRewarded => onRewarded;
        public IReadOnlyReactiveProperty<bool> IsCompleted => buttonVM.CounterVM.IsCompleted;

        [Inject]
        public void Inject(IAdsService ads, AdsVMFactory adsVMF)
        {
            this.ads = ads;
            this.adsVMF = adsVMF;
        }

        private void Awake()
        {
            button.OnClick
                .Subscribe(ButtonClickCallback)
                .AddTo(this);
        }

        public void Init(CompositeDisp disp)
        {
            buttonVM = adsVMF.GetButton(key);
            buttonVM.AddTo(disp);

            globalCooldownVM = adsVMF.GetGlobalCooldown();
            globalCooldownVM.AddTo(disp);

            adsViewedCounter.Init(buttonVM.CounterVM, disp);

            buttonVM.CounterVM.IsCompleted
                .Subscribe(x => CounterCompletedCallback(x, disp))
                .AddTo(disp);

            HideLoading();
        }

        private void CounterCompletedCallback(bool isCompleted, CompositeDisp disp)
        {
            activeTimerDisp.Clear();
            activeTimerDisp.AddTo(disp);

            activeCooldownVM = isCompleted
                ? buttonVM.CounterVM.CooldownVM
                : globalCooldownVM;

            activeCooldownVM.TimeLeft
                .SilentSubscribe(UpdateButtonState)
                .AddTo(activeTimerDisp);

            adsCooldown.Init(activeCooldownVM, activeTimerDisp);

            UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            var timeLeft = activeCooldownVM.TimeLeft.Value;

            button.SetInteractableState(timeLeft <= 0);
        }

        private async void ButtonClickCallback()
        {
            if (inProcess)
            {
                return;
            }

            ShowLoading();

            var result = await ads.ShowRewarded(key);

            if (result is { Success: true, Value: AdsResult.Success })
            {
                onRewarded.OnNext();
            }

            HideLoading();
        }

        private void ShowLoading()
        {
            loadingIndicator.SetActive(true);
            inProcess = true;
        }

        private void HideLoading()
        {
            loadingIndicator.SetActive(false);
            inProcess = false;
        }
    }
}