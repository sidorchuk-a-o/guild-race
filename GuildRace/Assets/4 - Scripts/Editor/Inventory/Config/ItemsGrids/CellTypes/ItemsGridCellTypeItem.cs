using AD.ToolsCollection;

namespace Game.Inventory
{
    /// <summary>
    /// Item: <see cref="ItemsGridCellTypeData"/>
    /// </summary>
    public class ItemsGridCellTypeItem : EntityListItemElement
    {
        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            titleLabel.editButtonOn = true;
        }
    }
}