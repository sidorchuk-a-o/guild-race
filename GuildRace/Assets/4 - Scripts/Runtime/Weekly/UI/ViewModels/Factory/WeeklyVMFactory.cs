using AD.Services.Router;

namespace Game.Weekly
{
    public class WeeklyVMFactory : VMFactory
    {
        private readonly WeeklyConfig weeklyConfig;
        private readonly IWeeklyService weeklyService;

        public WeeklyVMFactory(WeeklyConfig weeklyConfig, IWeeklyService weeklyService)
        {
            this.weeklyConfig = weeklyConfig;
            this.weeklyService = weeklyService;
        }

        public WeeklyVM GetWeekly()
        {
            return new WeeklyVM(weeklyConfig, weeklyService);
        }
    }
}