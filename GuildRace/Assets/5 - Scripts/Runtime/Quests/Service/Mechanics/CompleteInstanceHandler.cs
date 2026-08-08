using AD.Game.Instances;
using AD.ToolsCollection;
using AD.UI;
using R3;
using VContainer;

namespace AD.Game.Quests
{
    public class CompleteInstanceHandler : QuestMechanicHandler
    {
        private const int InstanceIdIndex = 0;

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
                var instanceId = quest.MechanicParams[InstanceIdIndex].IntParse();

                if (instance.Instance.Id != instanceId)
                {
                    continue;
                }

                var bossId = instance.BossUnit.Id;

                quest.AddData(bossId.ToString(), string.Empty);
                quest.SetProgress(quest.Data.Count);
            }
        }

        public override UITextData GetDescKey(QuestInfo quest)
        {
            var instanceId = quest.MechanicParams[InstanceIdIndex].IntParse();
            var instanceData = instancesConfig.GetInstance(instanceId);

            var data = new object[]
            {
                instanceData.NameKey
            };

            return new(descKey, data);
        }
    }
}