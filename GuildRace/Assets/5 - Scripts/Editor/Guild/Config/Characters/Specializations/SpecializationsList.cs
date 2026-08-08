using System;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    public class SpecializationsList : ListElement<SpecializationData>
    {
        protected override Type ItemType { get; } = typeof(SpecializationItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowAddButton = false;
            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}