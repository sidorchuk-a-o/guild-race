﻿using AD.Services;
using Cysharp.Threading.Tasks;
using System;
using VContainer;

namespace Game.Weekly
{
    public class WeeklyService : Service, IWeeklyService
    {
        private readonly WeeklyConfig config;
        private readonly WeeklyState state;

        public int CurrentWeek => state.CurrentWeek;
        public int CurrentDayOfWeek => state.CurrentDayOfWeek;

        public WeeklyService(WeeklyConfig config, IObjectResolver resolver)
        {
            this.config = config;

            state = new(config, resolver);
        }

        public override async UniTask<bool> Init()
        {
            state.Init();

            TryResetWeek();

            return await Inited();
        }

        private void TryResetWeek()
        {
            var currentDay = (DateTime.Today - new DateTime(2024, 1, 1)).TotalDays;
            var currentWeek = (int)currentDay / config.DaysCount;

            if (currentWeek != state.CurrentWeek)
            {
                state.SetWeek(currentWeek);
            }

            var firstDayOfWeek = currentWeek * config.DaysCount;
            var currentDayOfWeek = (int)currentDay - firstDayOfWeek;

            state.SetDayOfWeek(currentDayOfWeek);
        }
    }
}