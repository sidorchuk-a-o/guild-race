using System.Linq;
using AD.ToolsCollection;
using AD.Services.Leaderboards;
using Game.Guild;
using UniRx;

namespace Game.Instances
{
    public class InstanceLeaderboardModule
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
                .Where(x => x.BossUnit.CompletedCount.Value == 1)
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

            var score = leaderboard.Score.Value + scoreParams.Score;
            var extraData = guildService.CreateLeaderboeardExtraData();

            leaderboards.SendScore(leaderboardKey, score, extraData);
        }
    }
}