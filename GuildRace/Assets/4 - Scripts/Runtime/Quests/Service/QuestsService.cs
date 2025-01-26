using AD.Services;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using VContainer;

namespace Game.Quests
{
    public class QuestsService : Service, IQuestsService
    {
        private readonly IObjectResolver resolver;

        private readonly IReadOnlyList<QuestsGroupModule> modules;
        private readonly IReadOnlyList<QuestMechanicHandler> mechanicHandlers;

        public IEnumerable<QuestInfo> Quests => modules.SelectMany(x => x.Quests);
        public IEnumerable<QuestsGroupModule> Modules => modules;

        public QuestsService(QuestsConfig config, IObjectResolver resolver)
        {
            this.resolver = resolver;

            modules = config.GroupModules.ToList();
            mechanicHandlers = config.MechanicHandlers.ToList();
        }

        public override async UniTask<bool> Init()
        {
            InitModules();
            InitMechanicHandlers();

            return await Inited();
        }

        private void InitModules()
        {
            modules.ForEach(module =>
            {
                resolver.Inject(module);

                module.Init();
            });
        }

        private void InitMechanicHandlers()
        {
            mechanicHandlers.ForEach(handler =>
            {
                resolver.Inject(handler);

                handler.Init();
            });
        }

        public QuestsGroupModule GetModule(QuestsGroup id)
        {
            return modules.FirstOrDefault(x => x.Id == id);
        }

        public QuestMechanicHandler GetMechanicHandler(int id)
        {
            return mechanicHandlers.FirstOrDefault(x => x.Id == id);
        }
    }
}