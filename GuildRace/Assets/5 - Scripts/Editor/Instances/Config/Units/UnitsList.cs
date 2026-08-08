using System;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class UnitsList : ListElement<UnitData>
    {
        protected override Type ItemType { get; } = typeof(UnitItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Units";

            ShowAddButton = false;
            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}