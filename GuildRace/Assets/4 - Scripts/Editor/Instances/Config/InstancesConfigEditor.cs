using AD.ToolsCollection;
using UnityEditor;
using UnityEngine.UIElements;

namespace Game.Instances
{
    [ConfigEditor(typeof(InstancesConfig))]
    public class InstancesConfigEditor : ConfigEditor
    {
        private SeasonsList seasonsList;
        private InstanceTypesList instanceTypesList;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Seasons", CreateSeasonsTab);
            tabs.CreateTab("Common", CreateCommonTab);
        }

        private void CreateSeasonsTab(VisualElement root, SerializedData data)
        {
            seasonsList = root.CreateElement<SeasonsList>();
            seasonsList.FlexGrow(1).MaxWidth(33, LengthUnit.Percent).MarginRight(10);
            seasonsList.BindProperty("seasons", data);
        }

        private void CreateCommonTab(VisualElement root, SerializedData data)
        {
            root.ConvertToRow();

            instanceTypesList = root.CreateElement<InstanceTypesList>();
            instanceTypesList.FlexGrow(1).MaxWidth(33, LengthUnit.Percent).MarginRight(10);
            instanceTypesList.BindProperty("instanceTypes", data);
        }

        // == Menu ==

        [MenuItem("Game Services/Game/Instances")]
        public static InstancesConfigEditor GoToEditor()
        {
            return GoToEditor<InstancesConfigEditor>(width: 1200, height: 900);
        }

        public static InstancesConfigEditor GoToEditor(InstancesConfig config)
        {
            return GoToEditor<InstancesConfigEditor>(config, width: 1200, height: 900);
        }
    }
}