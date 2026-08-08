using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class EquipGroupsList : ListElement<EquipGroupData>
    {
        protected override Type ItemType { get; } = typeof(EquipGroupItem);
        protected override Type WizardType { get; } = typeof(EquipGroupImportWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Equip Groups";

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}