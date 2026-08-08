using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class EquipSlotsList : ItemSlotsList<EquipSlotData>
    {
        protected override Type WizardType { get; } = typeof(EquipSlotImportWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Equip Slots";
        }
    }
}