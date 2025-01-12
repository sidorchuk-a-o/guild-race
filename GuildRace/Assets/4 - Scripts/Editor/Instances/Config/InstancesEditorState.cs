using AD.ToolsCollection;
using UnityEditor.Callbacks;

namespace Game.Instances
{
    public class InstancesEditorState : EditorState<InstancesEditorState>
    {
        private InstancesConfig config;
        private InstancesEditorsFactory editorsFactory;

        public static InstancesConfig Config => FindAsset(ref instance.config);
        public static InstancesEditorsFactory EditorsFactory => instance.editorsFactory ??= new();

        private void OnEnable()
        {
            SaveEditorUtils.OnStartSavePropcess -= UpdateScriptFiles;
            SaveEditorUtils.OnStartSavePropcess += UpdateScriptFiles;
        }

        [DidReloadScripts]
        private static void CheckScriptFiles()
        {
            if (Config == null)
            {
                return;
            }

            KeyScriptFileUtils.CheckScriptFile(GetInstanceTypeScriptData());
        }

        private static void UpdateScriptFiles()
        {
            KeyScriptFileUtils.UpdateScriptFile(GetInstanceTypeScriptData());
        }

        // == Collections ==

        private static KeyScriptData<int> GetInstanceTypeScriptData() => new()
        {
            keyTypeName = nameof(InstanceType),
            namespaceValue = "Game.Instances",
            getCollection = GetInstanceTypesCollection
        };

        public static Collection<int> GetInstanceTypesCollection()
        {
            return Config.CreateKeyCollection<InstanceTypeData, int>("instanceTypes");
        }

        public static Collection<int> GetInstanceTypesViewCollection()
        {
            return Config.CreateKeyViewCollection<InstanceTypeData, int>("instanceTypes");
        }
    }
}