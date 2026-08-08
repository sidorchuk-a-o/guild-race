using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ItemByIdList : ListElement<int>
    {
        protected override Type ItemType { get; } = typeof(ItemIdItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
        }
    }
}