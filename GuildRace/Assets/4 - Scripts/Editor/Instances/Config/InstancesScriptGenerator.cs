using AD.ToolsCollection;

namespace Game.Instances
{
    public class InstancesScriptGenerator : KeyScriptGenerator
    {
        public override bool ConfigExist => InstancesEditorState.Config;

        protected override void UpdateScriptFiles()
        {
            UpdateScriptFile(GetInstanceTypeScriptData());
        }

        private static KeyScriptData<int> GetInstanceTypeScriptData() => new()
        {
            KeyTypeName = nameof(InstanceType),
            NamespaceValue = "Game.Instances",
            GetCollection = InstancesEditorState.GetInstanceTypesCollection
        };
    }
}