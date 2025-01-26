using VContainer;

namespace Game.Quests
{
    public class WeeklyQuestsState : QuestsGroupState<WeeklyQuestsSM>
    {
        public override string SaveKey => WeeklyQuestsSM.key;

        public int LastResetWeek { get; private set; }

        public WeeklyQuestsState(QuestsConfig questsConfig, IObjectResolver resolver) : base(questsConfig, resolver)
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

        protected override void ReadSave(WeeklyQuestsSM save)
        {
            base.ReadSave(save);

            if (save != null)
            {
                LastResetWeek = save.LastResetWeek;
            }
        }
    }
}