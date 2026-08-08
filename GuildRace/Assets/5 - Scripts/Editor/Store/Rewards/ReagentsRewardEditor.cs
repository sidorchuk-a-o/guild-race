using AD.Store;
using AD.ToolsCollection;

namespace AD.Game.Store
{
    [Menu("Reagents Reward")]
    [DataEditor(typeof(ReagentsReward))]
    public class ReagentsRewardEditor : RewardEditor
    {
        private ReagentRewardsList reagentRewardsList;

        protected override void CreateElementGUI(Element root)
        {
            reagentRewardsList = root.CreateElement<ReagentRewardsList>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            reagentRewardsList.BindProperty("reagentRewards", data);
        }
    }
}