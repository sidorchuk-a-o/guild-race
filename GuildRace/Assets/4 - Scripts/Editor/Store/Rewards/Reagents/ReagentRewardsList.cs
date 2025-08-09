using AD.ToolsCollection;

namespace Game.Store
{
    public class ReagentRewardsList : ListElement<ReagentRewardData, ReagentRewardItem>
    {
        public override void BindData(SerializedData data)
        {
            showCloneButton = false;

            base.BindData(data);
        }
    }
}