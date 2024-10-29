using AD.ToolsCollection;

namespace Game.Inventory
{
    [Menu("Reagent Items Factory")]
    [ItemVMEditor(typeof(ReagentItemsVMFactory))]
    public class ReagentItemsVMFactoryEditor : ItemsVMFactoryEditor
    {
    }
}