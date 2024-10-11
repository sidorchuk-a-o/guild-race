using AD.ToolsCollection;

namespace Game.Guild
{
    /// <summary>
    /// Item: <see cref="SpecializationData"/>
    /// </summary>
    public class SpecializationItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => GuildEditorState.EditorsFactory;
    }
}