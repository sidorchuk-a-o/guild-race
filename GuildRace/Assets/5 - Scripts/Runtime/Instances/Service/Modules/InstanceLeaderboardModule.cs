using AD.Game.Guild;
using AD.Leaderboards;
using AD.Management;
using R3;

namespace AD.Game.Instances
{
    public class InstanceLeaderboardModule : ServiceModule
    {
        private readonly InstancesConfig config;

        private readonly IGuildService guildService;
        private readonly ILeaderboardsService leaderboards;

        private LeaderboardInfo leaderboard;

        public InstanceLeaderboardModule(
            InstancesConfig config,
            ActiveInstanceModule activeInstanceModule,
            IGuildService guildService,
            ILeaderboardsService leaderboards)
        {
            this.config = config;
            this.guildService = guildService;
            this.leaderboards = leaderboards;

            activeInstanceModule.OnInstanceCompleted
                .Where(x => x.BossUnit.CompletedCount.CurrentValue == 1)
                .Subscribe(InstanceCompleted);
        }

        public void Init()
        {
            leaderboard = GetLeaderboard();
        }

        private LeaderboardInfo GetLeaderboard()
        {
            var leaderboardKey = config.LeaderboardParams.GuildScoreKey;

            return leaderboards.GetLeaderboard(leaderboardKey);
        }

        private void InstanceCompleted(ActiveInstanceInfo instanceInfo)
        {
            var leaderboardKey = config.LeaderboardParams.GuildScoreKey;

            var instanceType = instanceInfo.Instance.Type;
            var scoreParams = config.LeaderboardParams.GetScoreParams(instanceType);

            var score = leaderboard.Score.CurrentValue + scoreParams.Score;
            var extraData = guildService.CreateLeaderboardExtraData();

            leaderboards.SendScore(leaderboardKey, score, extraData);
        }
    }
}