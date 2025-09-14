using System.Linq;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using AD.Services;
using AD.Services.Analytics;
using AD.Services.Localization;
using AD.ToolsCollection;
using Game.GuildLevels;
using VContainer;
using UniRx;

namespace Game.Quests
{
    public class QuestsService : Service, IQuestsService
    {
        private readonly IGuildLevelsService guildLevelsService;
        private readonly IAnalyticsService analytics;
        private readonly IObjectResolver resolver;

        private readonly IReadOnlyList<QuestsGroupModule> groupModules;
        private readonly IReadOnlyList<QuestMechanicHandler> mechanicHandlers;

        private readonly QuestLevelContext levelContext;

        public IEnumerable<QuestInfo> Quests => groupModules.SelectMany(x => x.Quests);
        public IEnumerable<QuestsGroupModule> Modules => groupModules;
        public IReadOnlyReactiveProperty<float> RewardBonus => levelContext.RewardBonus;

        public QuestsService(
            QuestsConfig config,
            IGuildLevelsService guildLevelsService,
            IAnalyticsService analytics,
            ILocalizationService localization,
            IObjectResolver resolver)
        {
            this.resolver = resolver;
            this.guildLevelsService = guildLevelsService;
            this.analytics = analytics;

            groupModules = config.GroupModules.ToList();
            mechanicHandlers = config.MechanicHandlers.ToList();
            levelContext = new(config);

            QuestsAnalyticsExtensions.Init(this, localization);
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

            analytics.CompleteQuest(args.QuestId, group);
        }
    }
}