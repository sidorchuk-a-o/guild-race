using System;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class ConsumableTypesList : ListElement<ConsumableTypeData>
    {
        protected override Type ItemType { get; } = typeof(ConsumableTypeItem);
        protected override Type WizardType { get; } = typeof(ConsumableTypeImportWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Consumable Types";

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}