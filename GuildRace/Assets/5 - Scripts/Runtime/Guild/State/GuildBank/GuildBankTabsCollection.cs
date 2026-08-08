using System.Collections.Generic;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    public class GuildBankTabsCollection : ReactiveCollection<GuildBankTabInfo>, IGuildBankTabsCollection
    {
        public GuildBankTabsCollection(IEnumerable<GuildBankTabInfo> values) : base(values)
        {
        }
    }
}