using AD.DependencyInjection;
using AD.Services;

namespace Game.Guild
{
    [InstallerConfig(typeof(GuildConfig))]
    public class GuildServiceInstaller : ServiceInstaller<GuildService, IGuildService>
    {
    }
}