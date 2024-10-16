using AD.ToolsCollection;

namespace Game.Guild
{
    /// <summary>
    /// Item: <see cref="RoleData"/>
    /// </summary>
    public class RoleItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => GuildEditorState.EditorsFactory;
    }
}