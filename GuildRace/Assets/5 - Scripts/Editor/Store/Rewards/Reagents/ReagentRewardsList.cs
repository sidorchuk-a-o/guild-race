using System;
using AD.ToolsCollection;

namespace AD.Game.Store
{
    public class ReagentRewardsList : ListElement<ReagentRewardData>
    {
        protected override Type ItemType { get; } = typeof(ReagentRewardItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
        }
    }
}