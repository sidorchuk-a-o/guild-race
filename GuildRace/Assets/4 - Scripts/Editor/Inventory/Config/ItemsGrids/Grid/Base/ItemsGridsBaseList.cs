using AD.ToolsCollection;

namespace Game.Inventory
{
    public class ItemsGridsBaseList : ItemsGridsList<ItemsGridBaseData>
    {
        public override void BindData(SerializedData data)
        {
            wizardType = typeof(ItemsGridBaseCreateWizard);

            base.BindData(data);
        }
    }
}