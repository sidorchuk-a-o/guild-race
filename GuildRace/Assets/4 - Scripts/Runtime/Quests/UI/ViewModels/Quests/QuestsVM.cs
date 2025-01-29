using AD.Services.Router;

namespace Game.Quests
{
    public class QuestsVM : VMCollection<QuestInfo, QuestVM>
    {
        private readonly QuestsVMFactory questsVMF;

        public QuestsVM(IQuestsCollection values, QuestsVMFactory questsVMF) : base(values)
        {
            this.questsVMF = questsVMF;
        }

        protected override QuestVM Create(QuestInfo value)
        {
            return new QuestVM(value, questsVMF);
        }
    }
}