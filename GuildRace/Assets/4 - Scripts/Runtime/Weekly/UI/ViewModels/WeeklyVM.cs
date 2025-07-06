using AD.Services.Router;

namespace Game.Weekly
{
    public class WeeklyVM : ViewModel
    {
        public int CurrentDayOfWeek { get; }

        public WeeklyVM(IWeeklyService weeklyService)
        {
            CurrentDayOfWeek = weeklyService.CurrentDayOfWeek + 1;
        }

        protected override void InitSubscribes()
        {
        }
    }
}