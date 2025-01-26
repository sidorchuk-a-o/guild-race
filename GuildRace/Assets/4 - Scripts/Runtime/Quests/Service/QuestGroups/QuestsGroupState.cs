using AD.Services.Save;
using AD.States;
using System.Collections.Generic;
using VContainer;

namespace Game.Quests
{
    public abstract class QuestsGroupState<TSaveModel> : State<TSaveModel>
        where TSaveModel : QuestsGroupSM, new()
    {
        private readonly QuestsConfig questsConfig;

        private readonly QuestsCollection quests = new();

        public override SaveSource SaveSource => SaveSource.app;
        public IQuestsCollection Quests => quests;

        protected QuestsGroupState(QuestsConfig questsConfig, IObjectResolver resolver) : base(resolver)
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
            return new TSaveModel
            {
                Quests = Quests
            };
        }

        protected override void ReadSave(TSaveModel save)
        {
            if (save == null)
            {
                return;
            }

            quests.AddRange(save.GetQuests(questsConfig));
        }
    }
}