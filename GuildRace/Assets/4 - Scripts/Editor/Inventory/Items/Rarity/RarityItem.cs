using AD.ToolsCollection;

namespace Game.Inventory
{
    /// <summary>
    /// Item: <see cref="RarityData"/>
    /// </summary>
    public class RarityItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => InventoryEditorState.EditorsFactory;
    }
}