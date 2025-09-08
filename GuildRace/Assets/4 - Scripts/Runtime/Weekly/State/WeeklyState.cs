using AD.Services;
using AD.Services.Save;
using VContainer;
using UniRx;

namespace Game.Weekly
{
    public class WeeklyState : ServiceState<WeeklyConfig, WeeklySM>
    {
        private readonly ReactiveProperty<int> currentWeek = new();
        private readonly ReactiveProperty<int> currentDayOfWeek = new();

        public override string SaveKey => WeeklySM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public IReadOnlyReactiveProperty<int> CurrentWeek => currentWeek;
        public IReadOnlyReactiveProperty<int> CurrentDayOfWeek => currentDayOfWeek;

        public WeeklyState(WeeklyConfig config, IObjectResolver resolver) : base(config, resolver)
        {
        }

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
            return new WeeklySM
            {
                CurrentWeek = CurrentWeek.Value
            };
        }

        protected override void ReadSave(WeeklySM save)
        {
            if (save == null)
            {
                return;
            }

            currentWeek.Value = save.CurrentWeek;
        }
    }
}