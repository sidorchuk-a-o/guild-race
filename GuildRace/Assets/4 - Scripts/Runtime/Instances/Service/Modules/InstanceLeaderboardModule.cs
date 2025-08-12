using System.Collections.Generic;
using System.Linq;
using AD.Services.AppEvents;
using AD.Services.Leaderboards;
using Game.Guild;

namespace Game.Instances
{
    public class InstanceLeaderboardModule : IAppTickListener
    {
        private readonly InstancesState state;
        private readonly InstancesConfig config;

        private readonly IGuildService guildService;
        private readonly IAppEventsService appEvents;
        private readonly ILeaderboardsService leaderboards;

        private IReadOnlyDictionary<InstanceType, UnitInfo[]> bossesCache;

        private int totalScore;
        private float sendTimer;

        public InstanceLeaderboardModule(
            InstancesState state,
            InstancesConfig config,
            IGuildService guildService,
            ILeaderboardsService leaderboards,
            IAppEventsService appEvents)
        {
            this.state = state;
            this.config = config;
            this.guildService = guildService;
            this.leaderboards = leaderboards;
            this.appEvents = appEvents;
        }

        public void Init()
        {
            bossesCache = GetBossesCache();
            totalScore = GetCurrentScore();

            appEvents.AddAppTickListener(this);
        }

        private IReadOnlyDictionary<InstanceType, UnitInfo[]> GetBossesCache()
        {
            return state.Seasons
                .SelectMany(x => x.GetInstances())
                .SelectMany(x => x.BossUnits.Select(b => (type: x.Type, unit: b)))
                .GroupBy(x => x.type)
                .ToDictionary(x => x.Key, x => x.Select(t => t.unit).ToArray());
        }

        private int GetCurrentScore()
        {
            return bossesCache.Sum(getScore);

            int getScore(KeyValuePair<InstanceType, UnitInfo[]> kvp)
            {
                var scoreParams = config.LeaderboardParams.GetScoreParams(kvp.Key);
                var completedCount = kvp.Value.Sum(b => b.TotalCompletedCount);

                return scoreParams.Score * completedCount;
            }
        }

        void IAppTickListener.OnTick(float deltaTime)
        {
            sendTimer -= deltaTime;

            if (sendTimer > 0)
            {
                return;
            }

            sendTimer = config.LeaderboardParams.SendTime;

            var currentScore = GetCurrentScore();

            if (totalScore == currentScore)
            {
                return;
            }

            var guildName = guildService.GuildName.Value;
            var leaderboardKey = config.LeaderboardParams.GuildPowerKey;

            leaderboards.SendScore(leaderboardKey, currentScore, guildName);

            totalScore = currentScore;
        }

        void IAppTickListener.OnLateTick(float deltaTime)
        {
        }
    }
}