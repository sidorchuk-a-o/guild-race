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

        private readonly IReadOnlyList<QuestsGroupModule> groupModules;
        private readonly IReadOnlyList<QuestMechanicHandler> mechanicHandlers;

        public IEnumerable<QuestInfo> Quests => groupModules.SelectMany(x => x.Quests);
        public IEnumerable<QuestsGroupModule> Modules => groupModules;

        public QuestsService(QuestsConfig config, IObjectResolver resolver)
        {
            this.resolver = resolver;

            groupModules = config.GroupModules.ToList();
            mechanicHandlers = config.MechanicHandlers.ToList();
        }

        public override async UniTask<bool> Init()
        {
            InitGroupModules();
            InitMechanicHandlers();

            return await Inited();
        }

        private void InitGroupModules()
        {
            groupModules.ForEach(module =>
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

        public QuestsGroupModule GetGroupModule(QuestsGroup group)
        {
            return groupModules.FirstOrDefault(x => x.Id == group);
        }

        public QuestMechanicHandler GetMechanicHandler(int id)
        {
            return mechanicHandlers.FirstOrDefault(x => x.Id == id);
        }

        public void TakeQuestReward(TakeRewardArgs args)
        {
            var group = groupModules.FirstOrDefault(x => x.Id == args.GroupId);

            if (group == null)
            {
                return;
            }

            group.TakeQuestReward(args);
        }
    }
}