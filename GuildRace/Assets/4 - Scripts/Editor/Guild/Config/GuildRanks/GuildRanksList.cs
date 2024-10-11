using AD.ToolsCollection;

namespace Game.Guild
{
    public class GuildRanksList : ListElement<GuildRankData, GuildRankItem>
    {
        public override void BindData(SerializedData data)
        {
            showCloneButton = false;

            base.BindData(data);
        }
    }
}