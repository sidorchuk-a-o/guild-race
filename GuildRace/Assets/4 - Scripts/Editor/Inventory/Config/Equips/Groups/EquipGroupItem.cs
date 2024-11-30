using AD.ToolsCollection;

namespace Game.Inventory
{
    /// <summary>
    /// Item: <see cref="EquipGroupData"/>
    /// </summary>
    public class EquipGroupItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => InventoryEditorState.EditorsFactory;

        public override void BindData(SerializedData data)
        {
            foldoutOn = true;
            titleLabel.editButtonOn = true;

            base.BindData(data);
        }
    }
}