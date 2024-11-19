using AD.ToolsCollection;

namespace Game.Instances
{
    public class InstancesEditorState : EditorState<InstancesEditorState>
    {
        private InstancesConfig config;
        private InstancesEditorsFactory editorsFactory;

        public static InstancesConfig Config => FindAsset(ref instance.config);
        public static InstancesEditorsFactory EditorsFactory => instance.editorsFactory ??= new();

        public static Collection<int> GetInstanceTypesCollection()
        {
            return Config.CreateKeyViewCollection<InstanceTypeData, int>("instanceTypes");
        }
    }
}