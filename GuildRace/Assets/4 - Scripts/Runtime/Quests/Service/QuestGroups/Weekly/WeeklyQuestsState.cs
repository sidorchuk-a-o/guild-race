using Game.Weekly;
using VContainer;

namespace Game.Quests
{
    public class WeeklyQuestsState : QuestsGroupState<WeeklyQuestsSM>
    {
        private readonly IWeeklyService weeklyService;

        public override string SaveKey => WeeklyQuestsSM.key;

        public int LastResetWeek { get; private set; }

        public WeeklyQuestsState(QuestsConfig questsConfig, IWeeklyService weeklyService, IObjectResolver resolver)
            : base(questsConfig, resolver)
        {
            this.weeklyService = weeklyService;
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

            if (save == null)
            {
                LastResetWeek = weeklyService.CurrentWeek;
                return;
            }

            LastResetWeek = save.LastResetWeek;
        }
    }
}