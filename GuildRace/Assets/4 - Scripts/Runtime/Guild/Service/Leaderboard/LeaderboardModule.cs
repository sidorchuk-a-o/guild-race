using System.Linq;
using AD.Services.AppEvents;
using AD.Services.Leaderboards;

namespace Game.Guild
{
    public class LeaderboardModule : IAppTickListener
    {
        private readonly GuildState state;
        private readonly GuildConfig config;

        private readonly IAppEventsService appEvents;
        private readonly ILeaderboardsService leaderboards;

        private int powerScore;
        private float sendTimer;

        public LeaderboardModule(
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

            appEvents.AddAppTickListener(this);
        }

        private int GetCurrentScore()
        {
            return state.Characters.Sum(x => x.ItemsLevel.Value);
        }

        void IAppTickListener.OnTick(float deltaTime)
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

            var guildName = state.GuildName.Value;
            var leaderboardKey = config.LeaderboardParams.GuildPowerKey;

            leaderboards.SendScore(leaderboardKey, currentPower, guildName);

            powerScore = currentPower;
        }

        void IAppTickListener.OnLateTick(float deltaTime)
        {
        }
    }
}