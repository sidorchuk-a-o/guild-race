using AD.ToolsCollection;

namespace Game.Inventory
{
    /// <summary>
    /// Item: <see cref="EquipTypeData"/>
    /// </summary>
    public class EquipTypeItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => InventoryEditorState.EditorsFactory;
    }
}