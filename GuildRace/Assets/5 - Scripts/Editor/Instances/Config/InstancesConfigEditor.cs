using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    public class InstancesConfigEditor : ConfigEditor<InstancesConfig>
    {
        private SeasonsList seasonsList;
        private InstanceTypesList instanceTypesList;
        private UnitCooldownParamsList unitCooldownParamsList;
        private SquadParamsEditor squadParamsEditor;
        private CompleteChanceParamsEditor completeChanceParamsEditor;
        private ConsumablesParamsEditor consumablesParamsEditor;
        private RewardsParamsEditor rewardsParamsEditor;
        private ThreatsList threatsList;
        private LeaderboardParamsElement leaderboardParams;

        public override Vector2 Size { get; } = new(1200, 900);

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

            tabs.CreateTab("Seasons", CreateSeasonsTab);
            tabs.CreateTab("Common", CreateCommonTab);
            tabs.CreateTabs("Squad Params", CreateSquadParamsTab);
            tabs.CreateTabs("Complete Chance Params", CreateCompleteChanceParamsTab);
            tabs.CreateTabs("Consumables Params", CreateConsumablesParamsTab);
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

            leaderboardParams = root.CreateElement<LeaderboardParamsElement>();
            leaderboardParams.FlexWidth(33).MarginRight(10);
            leaderboardParams.BindProperty("leaderboardParams", data);
        }

        private void CreateSquadParamsTab(TabsContainer tabs)
        {
            squadParamsEditor = new();
            squadParamsEditor.CreateTabs(tabs);
        }

        private void CreateCompleteChanceParamsTab(TabsContainer tabs)
        {
            completeChanceParamsEditor = new();
            completeChanceParamsEditor.CreateTabs(tabs);
        }

        private void CreateConsumablesParamsTab(TabsContainer tabs)
        {
            consumablesParamsEditor = new();
            consumablesParamsEditor.CreateTabs(tabs);
        }

        private void CreateRewardsParamsTab(TabsContainer tabs)
        {
            rewardsParamsEditor = new();
            rewardsParamsEditor.CreateTabs(tabs);
        }

        private void CreateThreatsTab(VisualElement root, SerializedData data)
        {
            threatsList = root.CreateElement<ThreatsList>();
            threatsList.FlexWidth(33).MarginRight(10);
            threatsList.BindProperty("threats", data);
        }
    }
}