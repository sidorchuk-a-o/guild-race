using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class InstanceTypeGenerator : KeyScriptGenerator<InstanceType, int>
    {
        protected override bool ConfigExist => InstancesEditorState.Config;
        protected override string NamespaceValue => "AD.Game.Instances";

        protected override Collection<int> GetKeysCollection()
        {
            return InstancesEditorState.GetInstanceTypesCollection();
        }
    }
}