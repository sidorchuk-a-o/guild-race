using AD.ToolsCollection;

namespace Game.Guild
{
    /// <summary>
    /// Item: <see cref="DefaultCharacterData"/>
    /// </summary>
    public class DefaultCharacterItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => GuildEditorState.EditorsFactory;
    }
}