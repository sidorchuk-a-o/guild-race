using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class OptionKeyGenerator : KeyScriptGenerator<OptionKey, string>
    {
        protected override bool ConfigExist => InventoryEditorState.Config;
        protected override string NamespaceValue => "AD.Game.Inventory";

        protected override Collection<string> GetKeysCollection()
        {
            return InventoryEditorState.CreateOptionsCollection();
        }
    }
}