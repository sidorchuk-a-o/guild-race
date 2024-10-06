using UniRx;

namespace Game.Guild
{
    public interface IGuildService
    {
        bool GuildExists { get; }
        IReadOnlyReactiveProperty<string> Name { get; }

        void CreateGuild(GuildEM guildEM);
    }
}