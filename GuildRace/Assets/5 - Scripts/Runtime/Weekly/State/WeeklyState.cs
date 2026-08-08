using AD.Management;
using AD.Save;
using R3;

namespace AD.Game.Weekly
{
    public class WeeklyState : ServiceState<WeeklySM>
    {
        private readonly ReactiveProperty<int> currentWeek = new();
        private readonly ReactiveProperty<int> currentDayOfWeek = new();

        public override string SaveKey => "weekly";
        public override SaveSource SaveSource => SaveSource.App;

        public ReadOnlyReactiveProperty<int> CurrentWeek => currentWeek;
        public ReadOnlyReactiveProperty<int> CurrentDayOfWeek => currentDayOfWeek;

        public void SetWeek(int value)
        {
            currentWeek.Value = value;

            MarkAsDirty();
        }

        public void SetDayOfWeek(int value)
        {
            currentDayOfWeek.Value = value;
        }

        // == Save ==

        protected override WeeklySM CreateSave()
        {
            return new()
            {
                CurrentWeek = CurrentWeek.CurrentValue
            };
        }

        protected override void CreateDefaultState()
        {
        }

        protected override void ReadSave(WeeklySM save)
        {
            currentWeek.Value = save.CurrentWeek;
        }
    }
}