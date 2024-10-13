using UniRx;

namespace Game.Guild
{
    public class GuildRankInfo
    {
        private readonly ReactiveProperty<string> name = new();

        public GuildRankId Id { get; }
        public IReadOnlyReactiveProperty<string> Name => name;

        public GuildRankInfo(GuildRankId id, string name)
        {
            Id = id;

            this.name.Value = name;
        }

        public void SetName(string value)
        {
            name.Value = value;
        }
    }
}