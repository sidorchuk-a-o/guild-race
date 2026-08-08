using System;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    public class GuildBankTabsList : ListElement<GuildBankTabData>
    {
        protected override Type ItemType { get; } = typeof(GuildBankTabItem);
    }
}