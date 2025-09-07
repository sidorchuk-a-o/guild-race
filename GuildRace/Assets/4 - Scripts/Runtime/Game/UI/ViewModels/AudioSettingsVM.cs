using AD.Services.Audio;
using AD.Services.Router;
using UniRx;

namespace Game
{
    public class AudioSettingsVM : ViewModel
    {
        private readonly IAudioService audioService;

        public IReadOnlyReactiveProperty<bool> MusicEnabled { get; }
        public IReadOnlyReactiveProperty<bool> UiEnabled { get; }

        public AudioSettingsVM(IAudioService audioService)
        {
            this.audioService = audioService;

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
    }
}