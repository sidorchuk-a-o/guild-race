using AD.ToolsCollection;

namespace Game.Craft
{
    public class ProductsList : ListElement<ProductData, ProductItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Products";

            showAddButton = false;
            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}