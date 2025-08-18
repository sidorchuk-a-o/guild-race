using AD.DependencyInjection;
using AD.Services;

namespace Game.GuildLevels
{
    [InstallerConfig(typeof(GuildLevelsConfig))]
    public class GuildLevelsServiceInstaller : ServiceInstaller<GuildLevelsService, IGuildLevelsService>
    {
    }
}