using AD.ToolsCollection;
using Game.Inventory;

namespace Game.Craft
{
    [Menu("Reagent Items Factory")]
    [ItemVMEditor(typeof(ReagentItemsVMFactory))]
    public class ReagentItemsVMFactoryEditor : ItemsVMFactoryEditor
    {
    }
}