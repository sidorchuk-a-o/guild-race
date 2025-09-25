using AD.UI;
using AD.Services.Router;
using AD.ToolsCollection;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using UniRx;

namespace Game
{
    public class SettingsContainer : UIContainer
    {
        [Header("Audio")]
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Toggle uiSoundsToggle;
        [SerializeField] private UISlider musicSlider;

        private AudioSettingsVM audioVM;

        [Inject]
        public void Inject(SettingsVMFactory settingsVM)
        {
            audioVM = settingsVM.GetAudio();
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            audioVM.AddTo(disp);

            // sound
            audioVM.UiEnabled
                .Subscribe(x => uiSoundsToggle.isOn = x)
                .AddTo(disp);

            uiSoundsToggle
                .OnValueChangedAsObservable()
                .Subscribe(audioVM.SetUiGlobalState)
                .AddTo(disp);

            // music
            audioVM.MusicEnabled
                .Subscribe(x => musicToggle.isOn = x)
                .AddTo(disp);

            musicToggle
                .OnValueChangedAsObservable()
                .Subscribe(audioVM.SetMusicGlobalState)
                .AddTo(disp);

            // volume
            audioVM.MusicVolume
                .Subscribe(x => musicSlider.SetValue(x))
                .AddTo(disp);

            musicSlider.OnValueChanged
                .Subscribe(audioVM.SetMusicVolume)
                .AddTo(disp);
        }
    }
}