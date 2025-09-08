using AD.Services.Router;
using UniRx;

namespace Game.Weekly
{
    public class WeeklyVM : ViewModel
    {
        private readonly IWeeklyService weeklyService;
        private readonly ReactiveProperty<int> currentDayOfWeek = new();

        public string DaysCount { get; }
        public IReadOnlyReactiveProperty<int> CurrentDayOfWeek => currentDayOfWeek;

        public WeeklyVM(WeeklyConfig weeklyConfig, IWeeklyService weeklyService)
        {
            this.weeklyService = weeklyService;

            DaysCount = weeklyConfig.DaysCount.ToString();
        }

        protected override void InitSubscribes()
        {
            weeklyService.CurrentDayOfWeek
                .Subscribe(x => currentDayOfWeek.Value = x + 1)
                .AddTo(this);
        }
    }
}