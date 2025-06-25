using AD.Services.Router;

namespace Game.Weekly
{
    public class WeeklyVMFactory : VMFactory
    {
        private readonly IWeeklyService weeklyService;

        public WeeklyVMFactory(IWeeklyService weeklyService)
        {
            this.weeklyService = weeklyService;
        }

        public WeeklyVM GetWeekly()
        {
            return new WeeklyVM(weeklyService);
        }
    }
}