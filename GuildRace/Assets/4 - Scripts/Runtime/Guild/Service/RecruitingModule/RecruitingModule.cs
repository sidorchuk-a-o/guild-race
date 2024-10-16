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

        public IReadOnlyReactiveProperty<bool> IsEnabled => state.RecruitingState.IsEnabled;

        public IJoinRequestsCollection Requests => state.RecruitingState.Requests;
        public IReadOnlyList<ClassRoleSelectorInfo> ClassRoleSelectors => state.RecruitingState.ClassRoleSelectors;

        public RecruitingModule(GuildConfig config, GuildState state, ITimeService time)
        {
            this.config = config;
            this.state = state;
            this.time = time;

            data = config.RecruitingModule;
        }

        public void Init()
        {
            UpdateOffileRequests();

            time.OnTick.Subscribe(OnUpdate);
        }

        public void SwitchRecruitingState()
        {
            state.RecruitingState.SwitchRecruitingState();
        }

        private void UpdateOffileRequests()
        {
            if (!IsEnabled.Value)
            {
                return;
            }

            TryRemoveOldRequests();

            // check next request time

            var currentTime = DateTime.Now;
            var recruitingState = state.RecruitingState;

            if (recruitingState.NextRequestTime > currentTime)
            {
                return;
            }

            // new requests

            var deltaTime = (int)(currentTime - recruitingState.NextRequestTime).TotalSeconds;
            var midRequestTime = (data.MaxNextRequestTime - data.MinNextRequestTime) / 2f;
            var possibleRequestCount = Mathf.RoundToInt(deltaTime / midRequestTime);

            var requestCount = recruitingState.Requests.Count;
            var maxRequestCount = Random.Range(requestCount, CalcMaxRequestCount());

            var newRequestCount = Mathf.Min(maxRequestCount - requestCount, possibleRequestCount);

            for (var i = 0; i < newRequestCount; i++)
            {
                var seconds = Random.Range(0, deltaTime);
                var createTime = recruitingState.NextRequestTime.AddSeconds(seconds);

                recruitingState.CreateRequest(createTime);
            }

            // next request time

            if (newRequestCount > 0)
            {
                var randomRequestTime = Random.Range(data.MinNextRequestTime, data.MaxNextRequestTime);
                var nextRequestTime = currentTime.AddSeconds(randomRequestTime);

                recruitingState.SetNextRequestTime(nextRequestTime);
            }
        }

        private void OnUpdate(TimeTick tick)
        {
            if (!IsEnabled.Value)
            {
                return;
            }

            TryRemoveOldRequests();

            TryCreateNewRequests();
        }

        private void TryRemoveOldRequests()
        {
            var recruitingState = state.RecruitingState;
            var requestLifetime = data.RequestLifetime;

            var requests = recruitingState.Requests;
            var requestCount = requests.Count;

            var currentTime = DateTime.Now;

            for (var i = requestCount - 1; i >= 0; i--)
            {
                var request = requests[i];
                var removeTime = request.CreatedTime.AddSeconds(requestLifetime);

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

            var currentTime = DateTime.Now;

            if (recruitingState.NextRequestTime > currentTime)
            {
                return;
            }

            var randomRequestTime = Random.Range(data.MinNextRequestTime, data.MaxNextRequestTime);
            var nextRequestTime = currentTime.AddSeconds(randomRequestTime);

            recruitingState.CreateRequest();
            recruitingState.SetNextRequestTime(nextRequestTime);
        }

        private int CalcMaxRequestCount()
        {
            var rosterCount = state.Characters.Count;
            var maxRosterCount = config.MaxCharactersCount;

            var minRequestCount = data.MinRequestCount;
            var maxRequestCount = data.MaxRequestCount;

            var rosterRatio = (float)rosterCount / maxRosterCount;
            var addRequestCount = Mathf.RoundToInt((maxRequestCount - minRequestCount) * rosterRatio);

            return minRequestCount + addRequestCount;
        }
    }
}