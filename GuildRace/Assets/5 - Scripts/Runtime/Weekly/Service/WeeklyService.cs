using System;
using AD.AppEvents;
using AD.Management;
using Cysharp.Threading.Tasks;
using R3;

namespace AD.Game.Weekly
{
    public class WeeklyService : Service, IWeeklyService, IAppTickHandler
    {
        private readonly WeeklyState state;
        private readonly WeeklyConfig config;
        private readonly IAppEventsService appEvents;

        public ReadOnlyReactiveProperty<int> CurrentWeek => state.CurrentWeek;
        public ReadOnlyReactiveProperty<int> CurrentDayOfWeek => state.CurrentDayOfWeek;

        public WeeklyService(WeeklyConfig config, WeeklyState state, IAppEventsService appEvents)
        {
            this.state = state;
            this.config = config;
            this.appEvents = appEvents;
        }

        public override async UniTask<bool> Init()
        {
            TryResetWeek();

            appEvents.AddAppTickHandler(this);

            return await Inited();
        }

        void IAppTickHandler.OnTick(float deltaTime)
        {
            TryResetWeek();
        }

        private void TryResetWeek()
        {
            var currentDay = (DateTime.Today - new DateTime(2025, 1, 1)).TotalDays;
            var currentWeek = (int)currentDay / config.DaysCount;

            var firstDayOfWeek = currentWeek * config.DaysCount;
            var currentDayOfWeek = (int)currentDay - firstDayOfWeek;

            if (currentWeek != state.CurrentWeek.CurrentValue)
            {
                state.SetWeek(currentWeek);
            }

            if (currentDayOfWeek != state.CurrentDayOfWeek.CurrentValue)
            {
                state.SetDayOfWeek(currentDayOfWeek);
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            appEvents.RemoveAppTickHandler(this);
        }
    }
}