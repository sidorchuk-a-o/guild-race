using AD.ToolsCollection;

namespace Game.Inventory
{
    /// <summary>
    /// Item: <see cref="ItemsGridCategoryData"/>
    /// </summary>
    public class ItemsGridCategoryItem : EntityListItemElement
    {
        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            titleLabel.editButtonOn = true;
        }
    }
}