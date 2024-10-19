using AD.ToolsCollection;

namespace Game.Items
{
    /// <summary>
    /// Item: <see cref="EquipTypeData"/>
    /// </summary>
    public class EquipTypeItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => ItemsDatabaseEditorState.EditorsFactory;
    }
}