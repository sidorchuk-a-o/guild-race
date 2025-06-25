using AD.Services;
using AD.Services.Save;
using VContainer;

namespace Game.Weekly
{
    public class WeeklyState : ServiceState<WeeklyConfig, WeeklySM>
    {
        public override string SaveKey => WeeklySM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public int CurrentWeek { get; private set; }
        public int CurrentDayOfWeek { get; private set; }

        public WeeklyState(WeeklyConfig config, IObjectResolver resolver) : base(config, resolver)
        {
        }

        public void SetWeek(int value)
        {
            CurrentWeek = value;

            MarkAsDirty();
        }

        public void SetDayOfWeek(int value)
        {
            CurrentDayOfWeek = value;
        }

        // == Save ==

        protected override WeeklySM CreateSave()
        {
            return new WeeklySM
            {
                CurrentWeek = CurrentWeek
            };
        }

        protected override void ReadSave(WeeklySM save)
        {
            if (save == null)
            {
                return;
            }

            CurrentWeek = save.CurrentWeek;
        }
    }
}