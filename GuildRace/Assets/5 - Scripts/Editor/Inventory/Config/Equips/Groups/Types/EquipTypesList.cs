using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class EquipTypesList : ListElement<EquipTypeData>
    {
        protected override Type ItemType { get; } = typeof(EquipTypeItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Equip Types";

            ShowAddButton = false;
            ShowCloneButton = false;
            ShowRemoveButton = false;
            ShowRemoveButton = false;
        }
    }
}