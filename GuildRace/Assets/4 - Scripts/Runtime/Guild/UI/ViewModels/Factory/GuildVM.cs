using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;

namespace Game.Guild
{
    public class GuildVM : VMBase
    {
        public IReadOnlyReactiveProperty<string> GuildName { get; }

        public IReadOnlyReactiveProperty<string> PlayerNickname { get; }
        public IReadOnlyReactiveProperty<string> PlayerRank { get; }

        public GuildVM(IGuildService guildService)
        {
            GuildName = guildService.Name;

            PlayerNickname = new ReactiveProperty<string>("< NICKNAME >");
            PlayerRank = guildService.GuildRanks.GuildMaster.Name;
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
        }
    }
}