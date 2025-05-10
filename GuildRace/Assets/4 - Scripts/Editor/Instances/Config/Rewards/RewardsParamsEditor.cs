using System.ComponentModel;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    /// <summary>
    /// Editor: <see cref="RewardsParams"/>
    /// </summary>
    public class RewardsParamsEditor
    {
        private InstanceRewardsList rewardsList;
        private RewardHandlersList rewardHandlersList;

        private SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("rewardsParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Rewards", CreateRewardsTab);
            tabs.CreateTab("Params", CreateParamsTab);
        }

        private void CreateRewardsTab(VisualElement root, SerializedData data)
        {
            rewardsList = root.CreateElement<InstanceRewardsList>();
            rewardsList.BindProperty("rewards", GetData(data));
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            rewardHandlersList = root.CreateElement<RewardHandlersList>();
            rewardHandlersList.BindProperty("rewardHandlers", GetData(data));
            rewardHandlersList.FlexWidth(33).MarginRight(10);
        }
    }
}