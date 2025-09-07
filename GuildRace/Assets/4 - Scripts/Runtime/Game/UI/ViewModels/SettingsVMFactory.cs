using AD.Services.Audio;
using AD.Services.Router;

namespace Game
{
    public class SettingsVMFactory : VMFactory
    {
        private readonly IAudioService audioService;

        public SettingsVMFactory(IAudioService audioService)
        {
            this.audioService = audioService;
        }

        public AudioSettingsVM GetAudio()
        {
            return new AudioSettingsVM(audioService);
        }
    }
}