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
        private UnitCooldownParamsList unitCooldownParamsList;
        private SquadParamsEditor squadParamsEditor;
        private CompleteChanceParamsEditor completeChanceParamsEditor;
        private ConsumablesParamsEditor consumablesParamsEditor;
        private RewardsParamsEditor rewardsParamsEditor;
        private ThreatsList threatsList;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Seasons", CreateSeasonsTab);
            tabs.CreateTab("Common", CreateCommonTab);
            tabs.CreateTabs("Squad Params", CreateSquadParamsTab);
            tabs.CreateTabs("Complete Chance Params", CreateCompleteChanceParamsTab);
            tabs.CreateTabs("Сonsumables Params", CreateСonsumablesParamsTab);
            tabs.CreateTabs("Rewards Params", CreateRewardsParamsTab);
            tabs.CreateTab("Threats", CreateThreatsTab);
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

            unitCooldownParamsList = root.CreateElement<UnitCooldownParamsList>();
            unitCooldownParamsList.FlexWidth(33).MarginRight(10);
            unitCooldownParamsList.BindProperty("unitCooldownParams", data);
        }

        private void CreateSquadParamsTab(TabsContainer tabs)
        {
            squadParamsEditor = new SquadParamsEditor();
            squadParamsEditor.CreateTabs(tabs);
        }

        private void CreateCompleteChanceParamsTab(TabsContainer tabs)
        {
            completeChanceParamsEditor = new CompleteChanceParamsEditor();
            completeChanceParamsEditor.CreateTabs(tabs);
        }

        private void CreateСonsumablesParamsTab(TabsContainer tabs)
        {
            consumablesParamsEditor = new ConsumablesParamsEditor();
            consumablesParamsEditor.CreateTabs(tabs);
        }

        private void CreateRewardsParamsTab(TabsContainer tabs)
        {
            rewardsParamsEditor = new RewardsParamsEditor();
            rewardsParamsEditor.CreateTabs(tabs);
        }

        private void CreateThreatsTab(VisualElement root, SerializedData data)
        {
            threatsList = root.CreateElement<ThreatsList>();
            threatsList.FlexWidth(33).MarginRight(10);
            threatsList.BindProperty("threats", data);
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