using AD.Services.ProtectedTime;
using AD.ToolsCollection;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Guild
{
    public class RecruitingModule : IRecruitingModule
    {
        private readonly RecruitingModuleData data;
        private readonly GuildConfig config;

        private readonly GuildState state;
        private readonly ITimeService time;

        public IJoinRequestsCollection Requests => state.RecruitingState.Requests;
        public IReadOnlyList<ClassWeightInfo> ClassWeights => state.RecruitingState.ClassWeights;

        public RecruitingModule(GuildConfig config, GuildState state, ITimeService time)
        {
            this.config = config;
            this.state = state;
            this.time = time;

            data = config.RecruitingModule;
        }

        public void Init()
        {
            time.OnTick.Subscribe(OnUpdate);
        }

        private void OnUpdate(TimeTick tick)
        {
            TryRemoveOldRequests();

            TryCreateNewRequests();
        }

        private void TryRemoveOldRequests()
        {
            var recruitingState = state.RecruitingState;
            var requestLifetime = config.RecruitingModule.RequestLifetime;

            var requests = recruitingState.Requests;
            var requestCount = requests.Count;

            var currentTime = DateTime.Now.Ticks;

            for (var i = requestCount - 1; i >= 0; i--)
            {
                var request = requests[i];
                var removeTime = request.CreatedTime + requestLifetime;

                if (request.IsDefault || removeTime > currentTime)
                {
                    continue;
                }

                recruitingState.RemoveRequest(requestIndex: i);
            }
        }

        private void TryCreateNewRequests()
        {
            var recruitingState = state.RecruitingState;

            var requestCount = recruitingState.Requests.Count;
            var maxRequestCount = CalcMaxRequestCount();

            if (requestCount >= maxRequestCount)
            {
                return;
            }

            var currentTime = DateTime.Now.Ticks;

            if (recruitingState.NextRequestTime > currentTime)
            {
                return;
            }

            var requestTime = Random.Range(data.MinNextRequestTime, data.MaxNextRequestTime);

            recruitingState.CreateRequest();
            recruitingState.SetNextRequestTime(currentTime + requestTime);
        }

        private int CalcMaxRequestCount()
        {
            var rosterCount = state.Characters.Count;
            var maxRosterCount = config.MaxCharactersCount;

            var minRequestCount = config.RecruitingModule.MinRequestCount;
            var maxRequestCount = config.RecruitingModule.MaxRequestCount;

            var rosterRatio = rosterCount / maxRosterCount;
            var addRequestCount = Mathf.RoundToInt((maxRequestCount - minRequestCount) * rosterRatio);

            return minRequestCount + addRequestCount;
        }
    }
}