using System.Collections.Generic;
using System.Linq;
using AD.Services;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.GuildLevels;
using UniRx;
using VContainer;

namespace Game.Quests
{
    public class QuestsService : Service, IQuestsService
    {
        private readonly IGuildLevelsService guildLevelsService;
        private readonly IObjectResolver resolver;

        private readonly IReadOnlyList<QuestsGroupModule> groupModules;
        private readonly IReadOnlyList<QuestMechanicHandler> mechanicHandlers;

        private readonly QuestLevelContext levelContext;

        public IEnumerable<QuestInfo> Quests => groupModules.SelectMany(x => x.Quests);
        public IEnumerable<QuestsGroupModule> Modules => groupModules;
        public IReadOnlyReactiveProperty<float> RewardBonus => levelContext.RewardBonus;

        public QuestsService(QuestsConfig config, IGuildLevelsService guildLevelsService, IObjectResolver resolver)
        {
            this.resolver = resolver;
            this.guildLevelsService = guildLevelsService;

            groupModules = config.GroupModules.ToList();
            mechanicHandlers = config.MechanicHandlers.ToList();
            levelContext = new(config);
        }

        public override async UniTask<bool> Init()
        {
            InitGroupModules();
            InitMechanicHandlers();

            // guild levels
            guildLevelsService.RegisterContext(levelContext);

            levelContext.QuestCounts.ForEach(x =>
            {
                UpgradeQuestCounts(x.Key, x.Value);
            });

            levelContext.QuestCounts
                .ObserveReplace()
                .Subscribe(UpgradeQuestCountsCallback);

            return await Inited();
        }

        private void UpgradeQuestCountsCallback(DictionaryReplaceEvent<QuestsGroup, int> args)
        {
            var groupKey = args.Key;
            var increaseValue = args.NewValue;

            UpgradeQuestCounts(groupKey, increaseValue);
        }

        private void UpgradeQuestCounts(QuestsGroup groupKey, int increaseValue)
        {
            var module = GetGroupModule(groupKey);
            var newCount = module.DefaultMaxQuestsCount + increaseValue;

            module.SetQuestsCount(newCount);
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
            return groupModules.FirstOrDefault(x => x.Group == group);
        }

        public QuestMechanicHandler GetMechanicHandler(int id)
        {
            return mechanicHandlers.FirstOrDefault(x => x.Id == id);
        }

        public void TakeQuestReward(TakeRewardArgs args)
        {
            var group = groupModules.FirstOrDefault(x => x.Id == args.GroupId);
            var bonusValue = levelContext.RewardBonus.Value;

            if (group == null)
            {
                return;
            }

            group.TakeQuestReward(args, bonusValue);
        }
    }
}