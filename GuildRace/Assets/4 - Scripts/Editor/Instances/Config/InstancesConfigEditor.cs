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
        private SquadParamsEditor squadParamsEditor;
        private ActiveInstanceParamsEditor activeInstanceParamsEditor;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Seasons", CreateSeasonsTab);
            tabs.CreateTab("Common", CreateCommonTab);
            tabs.CreateTabs("Squad Params", CreateSquadParamsTab);
            tabs.CreateTabs("Active Instance Params", CreateActiveInstanceParamsTab);
        }

        private void CreateSeasonsTab(VisualElement root, SerializedData data)
        {
            seasonsList = root.CreateElement<SeasonsList>();
            seasonsList.FlexWidth(33).MarginRight(10);
            seasonsList.BindProperty("seasons", data);
        }

        private void CreateCommonTab(VisualElement root, SerializedData data)
        {
            root.ConvertToRow();

            instanceTypesList = root.CreateElement<InstanceTypesList>();
            instanceTypesList.FlexWidth(33).MarginRight(10);
            instanceTypesList.BindProperty("instanceTypes", data);
        }

        private void CreateSquadParamsTab(TabsContainer tabs)
        {
            squadParamsEditor = new SquadParamsEditor();
            squadParamsEditor.CreateTabs(tabs);
        }

        private void CreateActiveInstanceParamsTab(TabsContainer tabs)
        {
            activeInstanceParamsEditor = new ActiveInstanceParamsEditor();
            activeInstanceParamsEditor.CreateTabs(tabs);
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