using AD.Game.Instances;
using AD.ToolsCollection;
using AD.UI;
using R3;
using VContainer;

namespace AD.Game.Quests
{
    public class KillingBossesHandler : QuestMechanicHandler
    {
        private const int TypeIdIndex = 0;

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
                var typeId = quest.MechanicParams[TypeIdIndex].IntParse();

                if (instance.Instance.Type != typeId)
                {
                    continue;
                }

                quest.AddProgress(1);
            }
        }

        public override UITextData GetNameKey(QuestInfo quest)
        {
            var typeId = quest.MechanicParams[TypeIdIndex].IntParse();
            var typeData = instancesConfig.GetInstanceType(typeId);

            return new(nameKey, typeData.NameKey);
        }

        public override UITextData GetDescKey(QuestInfo quest)
        {
            return new(descKey, quest.RequiredProgress);
        }
    }
}