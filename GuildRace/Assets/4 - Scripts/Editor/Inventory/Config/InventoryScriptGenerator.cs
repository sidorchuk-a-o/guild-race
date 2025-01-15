using AD.ToolsCollection;

namespace Game.Inventory
{
    public class InventoryScriptGenerator : KeyScriptGenerator
    {
        public override bool ConfigExist => InventoryEditorState.Config;

        protected override void UpdateScriptFiles()
        {
            UpdateScriptFile(GetOptionKeyScriptData());
        }

        private static KeyScriptData<string> GetOptionKeyScriptData() => new()
        {
            KeyTypeName = nameof(OptionKey),
            NamespaceValue = "Game.Inventory",
            GetCollection = InventoryEditorState.CreateOptionsCollection
        };
    }
}