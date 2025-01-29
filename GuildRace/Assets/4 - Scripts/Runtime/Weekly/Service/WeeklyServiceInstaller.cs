using AD.DependencyInjection;
using AD.Services;

namespace Game.Weekly
{
    [InstallerConfig(typeof(WeeklyConfig))]
    public class WeeklyServiceInstaller : ServiceInstaller<WeeklyService, IWeeklyService>
    {
    }
}