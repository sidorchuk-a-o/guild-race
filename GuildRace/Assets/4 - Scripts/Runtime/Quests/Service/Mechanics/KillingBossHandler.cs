using AD.ToolsCollection;
using AD.UI;
using Game.Instances;
using UniRx;
using VContainer;

namespace Game.Quests
{
    public class KillingBossHandler : QuestMechanicHandler
    {
        private const int bossIdIndex = 0;

        private InstancesConfig instancesConfig;
        private IInstancesService instancesService;

        [Inject]
        public void Inject(InstancesConfig instancesConfig, IInstancesService instancesService)
        {
            this.instancesConfig = instancesConfig;
            this.instancesService = instancesService;
        }

        public override void Init()
        {
            base.Init();

            instancesService.OnInstanceCompleted.Subscribe(InstanceCompleteCallback);
        }

        private void InstanceCompleteCallback(ActiveInstanceInfo instance)
        {
            foreach (var quest in Quests)
            {
                var bossId = quest.MechanicParams[bossIdIndex].IntParse();

                if (instance.BossUnit.Id != bossId)
                {
                    continue;
                }

                quest.AddProgress(1);
            }
        }

        public override UITextData GetDescKey(QuestInfo quest)
        {
            var bossId = quest.MechanicParams[bossIdIndex].IntParse();

            var bossData = instancesConfig.GetBossUnit(bossId);
            var instanceData = instancesConfig.GetBossInstance(bossId);

            return new UITextData(descKey, bossData.NameKey, instanceData.NameKey);
        }
    }
}