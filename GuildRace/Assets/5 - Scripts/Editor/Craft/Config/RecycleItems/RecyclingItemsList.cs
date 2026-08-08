using System;
using AD.ToolsCollection;

namespace AD.Game.Craft
{
    public class RecyclingItemsList : ListElement<RecyclingItemData>
    {
        protected override Type ItemType { get; } = typeof(RecyclingItemItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
        }
    }
}