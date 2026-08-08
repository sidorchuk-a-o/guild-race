using AD.Audio;
using AD.Router;
using R3;

namespace AD.Game
{
    public class AudioSettingsVM : ViewModel
    {
        private readonly IAudioService audioService;

        public ReadOnlyReactiveProperty<float> MusicVolume { get; }
        public ReadOnlyReactiveProperty<bool> MusicEnabled { get; }
        public ReadOnlyReactiveProperty<bool> UiEnabled { get; }

        public AudioSettingsVM(IAudioService audioService)
        {
            this.audioService = audioService;

            MusicVolume = audioService.MusicModule.Volume;
            MusicEnabled = audioService.MusicModule.Enabled;
            UiEnabled = audioService.UiModule.Enabled;
        }

        protected override void InitSubscribes()
        {
        }

        public void SetMusicGlobalState(bool value)
        {
            audioService.MusicModule.SetGlobalState(value);
        }

        public void SetUiGlobalState(bool value)
        {
            audioService.UiModule.SetGlobalState(value);
        }

        public void SetMusicVolume(float value)
        {
            audioService.MusicModule.SetVolume(value);
        }
    }
}