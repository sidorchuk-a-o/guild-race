using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ItemTypeGenerator : KeyScriptGenerator<ItemType, int>
    {
        protected override bool ConfigExist => InventoryEditorState.Config;
        protected override string NamespaceValue => "AD.Game.Inventory";

        protected override Collection<int> GetKeysCollection()
        {
            return InventoryEditorState.CreateItemTypesCollection();
        }
    }
}