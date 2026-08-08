using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public abstract class ItemSlotsList<TData> : ListElement<TData>
        where TData : ItemSlotData
    {
        protected override Type ItemType { get; } = typeof(ItemSlotItem<TData>);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}