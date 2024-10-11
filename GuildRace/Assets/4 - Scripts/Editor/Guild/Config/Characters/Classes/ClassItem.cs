using AD.ToolsCollection;

namespace Game.Guild
{
    /// <summary>
    /// Item: <see cref="ClassData"/>
    /// </summary>
    public class ClassItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => GuildEditorState.EditorsFactory;
    }
}