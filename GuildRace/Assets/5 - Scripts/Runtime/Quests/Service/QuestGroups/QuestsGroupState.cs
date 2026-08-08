using System.Collections.Generic;
using AD.Management;
using AD.Save;

namespace AD.Game.Quests
{
    public abstract class QuestsGroupState<TSaveModel> : ServiceState<TSaveModel>
        where TSaveModel : QuestsGroupSM, new()
    {
        private readonly QuestsConfig questsConfig;
        private readonly QuestsCollection quests = new();

        public override SaveSource SaveSource => SaveSource.App;
        public IQuestsCollection Quests => quests;

        protected QuestsGroupState(QuestsConfig questsConfig)
        {
            this.questsConfig = questsConfig;
        }

        // == Quests ==

        public void AddQuests(IEnumerable<QuestInfo> values)
        {
            quests.AddRange(values);

            MarkAsDirty();
        }

        public void ClearQuests()
        {
            quests.Clear();

            MarkAsDirty();
        }

        // == Save ==

        protected override TSaveModel CreateSave()
        {
            return new()
            {
                Quests = Quests
            };
        }

        protected override void ReadSave(TSaveModel save)
        {
            quests.AddRange(save.GetQuests(questsConfig));
        }
    }
}