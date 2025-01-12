using AD.Services.Router;
using AD.ToolsCollection;

namespace Game.Inventory
{
    [VMFactoryEditor(typeof(InventoryVMFactoryInstaller))]
    public class InventoryVMFactoryInstallerEditor : VMFactoryInstallerEditor
    {
        private ItemsVMFactoriesList itemsVMFactoriesList;
        private ItemSlotsVMFactoriesList itemSlotsVMFactoriesList;

        protected override void CreateElementGUI(Element root)
        {
            itemsVMFactoriesList = root.CreateElement<ItemsVMFactoriesList>();
            itemSlotsVMFactoriesList = root.CreateElement<ItemSlotsVMFactoriesList>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            itemsVMFactoriesList.BindProperty("itemsFactories", data);
            itemSlotsVMFactoriesList.BindProperty("itemSlotsFactories", data);
        }
    }
}