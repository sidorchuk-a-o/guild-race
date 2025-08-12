using System.Linq;
using System.Collections.Generic;
using AD.ToolsCollection;
using AD.Services.AppEvents;
using AD.Services.Leaderboards;
using Game.Guild;
using UniRx;

namespace Game.Instances
{
    public class InstanceLeaderboardModule
    {
        private readonly InstancesState state;
        private readonly InstancesConfig config;

        private readonly IGuildService guildService;
        private readonly IAppEventsService appEvents;
        private readonly ILeaderboardsService leaderboards;

        private LeaderboardInfo leaderboard;
        private IReadOnlyDictionary<InstanceType, UnitInfo[]> bossesCache;

        public InstanceLeaderboardModule(
            InstancesState state,
            InstancesConfig config,
            ActiveInstanceModule activeInstanceModule,
            IGuildService guildService,
            ILeaderboardsService leaderboards,
            IAppEventsService appEvents)
        {
            this.state = state;
            this.config = config;
            this.guildService = guildService;
            this.leaderboards = leaderboards;
            this.appEvents = appEvents;

            activeInstanceModule.OnInstanceCompleted
                .Where(x => x.BossUnit.CompletedCount.Value == 1)
                .Subscribe(InstanceCompleted);
        }

        public void Init()
        {
            leaderboard = GetLeaderboard();
            bossesCache = GetBossesCache();
        }

        private LeaderboardInfo GetLeaderboard()
        {
            var guildPowerKey = config.LeaderboardParams.GuildPowerKey;

            return leaderboards.GetLeaderboard(guildPowerKey);
        }

        private IReadOnlyDictionary<InstanceType, UnitInfo[]> GetBossesCache()
        {
            return state.Seasons
                .SelectMany(x => x.GetInstances())
                .SelectMany(x => x.BossUnits.Select(b => (type: x.Type, unit: b)))
                .GroupBy(x => x.type)
                .ToDictionary(x => x.Key, x => x.Select(t => t.unit).ToArray());
        }

        private void InstanceCompleted(ActiveInstanceInfo instanceInfo)
        {
            var guildName = guildService.GuildName.Value;
            var leaderboardKey = config.LeaderboardParams.GuildPowerKey;
            var currentScore = GetCurrentScore();

            leaderboards.SendScore(leaderboardKey, currentScore, guildName);
        }

        private int GetCurrentScore()
        {
            return bossesCache.Sum(getScore);

            int getScore(KeyValuePair<InstanceType, UnitInfo[]> kvp)
            {
                var scoreParams = config.LeaderboardParams.GetScoreParams(kvp.Key);
                var completedCount = kvp.Value.Count(x => x.CompletedCount.Value > 0);

                return scoreParams.Score * completedCount;
            }
        }
    }
}