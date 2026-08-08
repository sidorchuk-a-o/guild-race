using System;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    public class GuildRanksList : ListElement<GuildRankData>
    {
        protected override Type ItemType { get; } = typeof(GuildRankItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
        }
    }
}