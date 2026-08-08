using AD.Router;

namespace AD.Game.Guild
{
    public class GuildRanksVM : VMCollection<GuildRankInfo, GuildRankVM>
    {
        public GuildRanksVM(IGuildRanksCollection values) : base(values)
        {
        }

        protected override GuildRankVM Create(GuildRankInfo value)
        {
            return new(value);
        }
    }
}