using AD.Services.Router;
using AD.UI;

namespace Game.Quests
{
    public class QuestMechanicVM : ViewModel
    {
        private readonly QuestMechanicHandler handler;

        public QuestMechanicVM(QuestMechanicHandler handler)
        {
            this.handler = handler;
        }

        protected override void InitSubscribes()
        {
        }

        public UITextData GetNameKey(QuestInfo quest)
        {
            return handler.GetNameKey(quest);
        }

        public UITextData GetDescKey(QuestInfo quest)
        {
            return handler.GetDescKey(quest);
        }
    }
}