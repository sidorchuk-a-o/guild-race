using AD.States;
using System.Collections.Generic;

namespace Game.Guild
{
    public class GuildBankTabsCollection : ReactiveCollectionInfo<GuildBankTabInfo>, IGuildBankTabsCollection
    {
        public GuildBankTabsCollection(IEnumerable<GuildBankTabInfo> values) : base(values)
        {
        }
    }
}