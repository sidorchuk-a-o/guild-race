using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public abstract class ItemsGridsList<TData> : ListElement<TData>
        where TData : ItemsGridData
    {
        protected override Type ItemType { get; } = typeof(ItemsGridItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
        }
    }
}