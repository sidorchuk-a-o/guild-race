using AD.ToolsCollection;

namespace Game.Inventory
{
    public class InventoryScriptGenerator : KeyScriptGenerator
    {
        public override bool ConfigExist => InventoryEditorState.Config;

        protected override void UpdateScriptFiles()
        {
            UpdateScriptFile(GetOptionKeyScriptData());
            UpdateScriptFile(GetItemTypesScriptData());
        }

        private static KeyScriptData<string> GetOptionKeyScriptData() => new()
        {
            KeyTypeName = nameof(OptionKey),
            NamespaceValue = "Game.Inventory",
            GetCollection = InventoryEditorState.CreateOptionsCollection
        };

        private KeyScriptData<int> GetItemTypesScriptData() => new()
        {
            KeyTypeName = nameof(ItemType),
            NamespaceValue = "Game.Inventory",
            GetCollection = InventoryEditorState.CreateItemTypesCollection
        };
    }
}