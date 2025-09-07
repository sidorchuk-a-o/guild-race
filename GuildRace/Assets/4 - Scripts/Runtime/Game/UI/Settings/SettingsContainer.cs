using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game
{
    public class SettingsContainer : UIContainer
    {
        [Header("Audio")]
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Toggle uiSoundsToggle;

        private AudioSettingsVM audioVM;

        private void Awake()
        {
            musicToggle
                .OnValueChangedAsObservable()
                .Subscribe(audioVM.SetMusicGlobalState)
                .AddTo(this);

            uiSoundsToggle
                .OnValueChangedAsObservable()
                .Subscribe(audioVM.SetUiGlobalState)
                .AddTo(this);
        }

        [Inject]
        public void Inject(SettingsVMFactory settingsVM)
        {
            audioVM = settingsVM.GetAudio();
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            audioVM.AddTo(disp);

            audioVM.UiEnabled
                .Subscribe(x => uiSoundsToggle.isOn = x)
                .AddTo(disp);

            audioVM.MusicEnabled
                .Subscribe(x => musicToggle.isOn = x)
                .AddTo(disp);
        }
    }
}