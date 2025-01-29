using System;
using VContainer;

namespace Game.Quests
{
    public class DailyQuestsState : QuestsGroupState<DailyQuestsSM>
    {
        public override string SaveKey => DailyQuestsSM.key;

        public DateTime LastResetDate { get; private set; }

        public DailyQuestsState(QuestsConfig questsConfig, IObjectResolver resolver) : base(questsConfig, resolver)
        {
        }

        public void SetLastResetDate(DateTime value)
        {
            LastResetDate = value;

            MarkAsDirty();
        }

        // == Save ==

        protected override DailyQuestsSM CreateSave()
        {
            var save = base.CreateSave();

            save.LastResetDate = LastResetDate;

            return save;
        }

        protected override void ReadSave(DailyQuestsSM save)
        {
            base.ReadSave(save);

            if (save != null)
            {
                LastResetDate = save.LastResetDate;
            }
        }
    }
}