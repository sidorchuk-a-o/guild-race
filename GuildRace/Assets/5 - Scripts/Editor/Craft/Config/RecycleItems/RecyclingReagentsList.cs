using System;
using AD.ToolsCollection;

namespace AD.Game.Craft
{
    public class RecyclingReagentsList : ListElement<RecyclingReagentData>
    {
        protected override Type ItemType { get; } = typeof(RecyclingReagentItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
        }
    }
}