using AD.DependencyInjection;
using AD.Services;

namespace Game.Quests
{
    [InstallerConfig(typeof(QuestsConfig))]
    public class QuestsServiceInstaller : ServiceInstaller<QuestsService, IQuestsService>
    {
    }
}