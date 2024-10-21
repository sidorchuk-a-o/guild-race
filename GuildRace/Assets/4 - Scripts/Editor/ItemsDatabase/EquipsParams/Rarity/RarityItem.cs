using AD.ToolsCollection;

namespace Game.Items
{
    /// <summary>
    /// Item: <see cref="RarityData"/>
    /// </summary>
    public class RarityItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => ItemsEditorState.EditorsFactory;
    }
}