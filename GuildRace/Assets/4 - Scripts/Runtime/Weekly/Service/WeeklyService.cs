using AD.Services;
using AD.Services.AppEvents;
using System;
using Cysharp.Threading.Tasks;
using VContainer;
using UniRx;

namespace Game.Weekly
{
    public class WeeklyService : Service, IWeeklyService, IAppTickListener
    {
        private readonly WeeklyState state;
        private readonly WeeklyConfig config;
        private readonly IAppEventsService appEvents;

        public IReadOnlyReactiveProperty<int> CurrentWeek => state.CurrentWeek;
        public IReadOnlyReactiveProperty<int> CurrentDayOfWeek => state.CurrentDayOfWeek;

        public WeeklyService(WeeklyConfig config, IAppEventsService appEvents, IObjectResolver resolver)
        {
            this.config = config;
            this.appEvents = appEvents;

            state = new(config, resolver);
        }

        public override async UniTask<bool> Init()
        {
            state.Init();

            appEvents.AddAppTickListener(this);

            return await Inited();
        }

        void IAppTickListener.OnTick(float deltaTime)
        {
            TryResetWeek();
        }

        void IAppTickListener.OnLateTick(float deltaTime)
        {
        }

        private void TryResetWeek()
        {
            var currentDay = (DateTime.Today - new DateTime(2025, 1, 1)).TotalDays;
            var currentWeek = (int)currentDay / config.DaysCount;

            var firstDayOfWeek = currentWeek * config.DaysCount;
            var currentDayOfWeek = (int)currentDay - firstDayOfWeek;

            if (currentWeek != state.CurrentWeek.Value)
            {
                state.SetWeek(currentWeek);
            }

            if (currentDayOfWeek != state.CurrentDayOfWeek.Value)
            {
                state.SetDayOfWeek(currentDayOfWeek);
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            appEvents.RemoveAppTickListener(this);
        }
    }
}