using AD.ToolsCollection;

namespace Game.Inventory
{
    /// <summary>
    /// Item: <see cref="ItemSlotData"/>
    /// </summary>
    public class ItemSlotItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => InventoryEditorState.EditorsFactory;
    }
}