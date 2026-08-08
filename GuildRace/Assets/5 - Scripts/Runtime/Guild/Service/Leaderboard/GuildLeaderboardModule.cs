using System.Linq;
using AD.AppEvents;
using AD.Leaderboards;
using AD.Management;

namespace AD.Game.Guild
{
    public class GuildLeaderboardModule : ServiceModule, IAppTickHandler
    {
        private readonly GuildState state;
        private readonly GuildConfig config;

        private readonly IAppEventsService appEvents;
        private readonly ILeaderboardsService leaderboards;

        private int powerScore;
        private float sendTimer;

        public GuildLeaderboardModule(
            GuildState state,
            GuildConfig config,
            ILeaderboardsService leaderboards,
            IAppEventsService appEvents)
        {
            this.state = state;
            this.config = config;
            this.leaderboards = leaderboards;
            this.appEvents = appEvents;
        }

        public void Init()
        {
            powerScore = GetCurrentScore();

            appEvents.AddAppTickHandler(this);
        }

        private int GetCurrentScore()
        {
            return state.Characters.Sum(x => x.ItemsLevel.CurrentValue);
        }

        void IAppTickHandler.OnTick(float deltaTime)
        {
            sendTimer -= deltaTime;

            if (sendTimer > 0)
            {
                return;
            }

            sendTimer = config.LeaderboardParams.SendTime;

            var currentPower = GetCurrentScore();

            if (powerScore == currentPower)
            {
                return;
            }

            var extraData = state.CreateLeaderboardExtraData();
            var leaderboardKey = config.LeaderboardParams.GuildPowerKey;

            leaderboards.SendScore(leaderboardKey, currentPower, extraData);

            powerScore = currentPower;
        }
    }
}