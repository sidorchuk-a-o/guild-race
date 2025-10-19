using AD.Services.Localization;
using AD.Services.Router;

namespace Game.Guild
{
    public class GuildRankVM : ViewModel
    {
        public GuildRankId Id { get; }
        public LocalizeKey NameKey { get; }

        public GuildRankVM(GuildRankInfo info)
        {
            Id = info.Id;
            NameKey = info.NameKey;
        }

        protected override void InitSubscribes()
        {
        }
    }
}