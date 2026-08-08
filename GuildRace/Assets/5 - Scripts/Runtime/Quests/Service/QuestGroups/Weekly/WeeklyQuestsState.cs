namespace AD.Game.Quests
{
    public class WeeklyQuestsState : QuestsGroupState<WeeklyQuestsSM>
    {
        public override string SaveKey => "weekly-quests";

        public int LastResetWeek { get; private set; }

        public WeeklyQuestsState(QuestsConfig questsConfig) : base(questsConfig)
        {
        }

        public void SetResetWeek(int value)
        {
            LastResetWeek = value;
        }

        // == Save ==

        protected override WeeklyQuestsSM CreateSave()
        {
            var save = base.CreateSave();

            save.LastResetWeek = LastResetWeek;

            return save;
        }

        protected override void CreateDefaultState()
        {
        }

        protected override void ReadSave(WeeklyQuestsSM save)
        {
            base.ReadSave(save);

            LastResetWeek = save.LastResetWeek;
        }
    }
}