using AD.Services.Router;

namespace Game.Guild
{
    public class GuildRanksVM : VMCollection<GuildRankInfo, GuildRankVM>
    {
        public GuildRanksVM(IGuildRanksCollection values) : base(values)
        {
        }

        protected override GuildRankVM Create(GuildRankInfo value)
        {
            return new GuildRankVM(value);
        }
    }
}