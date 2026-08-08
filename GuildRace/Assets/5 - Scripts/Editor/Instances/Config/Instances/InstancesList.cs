using System;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class InstancesList : ListElement<InstanceData>
    {
        protected override Type ItemType { get; } = typeof(InstanceListItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowAddButton = false;
            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}