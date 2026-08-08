using AD.Audio;
using AD.Router;

namespace AD.Game
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
            return new(audioService);
        }
    }
}