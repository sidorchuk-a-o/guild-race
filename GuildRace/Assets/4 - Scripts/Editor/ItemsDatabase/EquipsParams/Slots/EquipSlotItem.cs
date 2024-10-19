using AD.ToolsCollection;

namespace Game.Items
{
    /// <summary>
    /// Item: <see cref="EquipSlotData"/>
    /// </summary>
    public class EquipSlotItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => ItemsDatabaseEditorState.EditorsFactory;
    }
}