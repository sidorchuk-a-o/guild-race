using System;

namespace AD.Game.Quests
{
    public class DailyQuestsState : QuestsGroupState<DailyQuestsSM>
    {
        public override string SaveKey => "daily-quests";

        public DateTime LastResetDate { get; private set; }

        public DailyQuestsState(QuestsConfig questsConfig) : base(questsConfig)
        {
        }

        public void SetLastResetDate(DateTime value)
        {
            LastResetDate = value;

            MarkAsDirty(true);
        }

        // == Save ==

        protected override DailyQuestsSM CreateSave()
        {
            var save = base.CreateSave();

            save.LastResetDate = LastResetDate;

            return save;
        }

        protected override void CreateDefaultState()
        {
        }

        protected override void ReadSave(DailyQuestsSM save)
        {
            base.ReadSave(save);

            LastResetDate = save.LastResetDate;
        }
    }
}