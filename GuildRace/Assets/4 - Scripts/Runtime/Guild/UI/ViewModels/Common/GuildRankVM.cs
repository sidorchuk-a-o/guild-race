using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;

namespace Game.Guild
{
    public class GuildRankVM : VMBase
    {
        public GuildRankId Id { get; }
        public IReadOnlyReactiveProperty<string> Name { get; }

        public GuildRankVM(GuildRankInfo info)
        {
            Id = info.Id;
            Name = info.Name;
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
        }
    }
}