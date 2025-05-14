using AD.ToolsCollection;
using AD.UI;
using Game.Instances;
using UniRx;
using VContainer;

namespace Game.Quests
{
    public class CompleteInstanceHandler : QuestMechanicHandler
    {
        private const int instanceIdIndex = 0;

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

            instancesService.OnInstanceCompleted.Subscribe(InstenceCompleteCallback);
        }

        private void InstenceCompleteCallback(ActiveInstanceInfo instance)
        {
            foreach (var quest in Quests)
            {
                var instanceId = quest.MechanicParams[instanceIdIndex].IntParse();

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
            var instanceId = quest.MechanicParams[instanceIdIndex].IntParse();
            var instanceData = instancesConfig.GetInstance(instanceId);

            var data = new object[]
            {
                instanceData.NameKey
            };

            return new UITextData(descKey, data);
        }
    }
}