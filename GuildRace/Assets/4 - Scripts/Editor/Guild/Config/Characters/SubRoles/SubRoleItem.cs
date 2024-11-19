using AD.ToolsCollection;

namespace Game.Guild
{
    /// <summary>
    /// Item: <see cref="SubRoleData"/>
    /// </summary>
    public class SubRoleItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => GuildEditorState.EditorsFactory;
    }
}