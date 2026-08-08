using System;
using AD.ToolsCollection;

namespace AD.Game.Craft
{
    public class VendorsList : ListElement<VendorData>
    {
        protected override Type WizardType { get; } = typeof(VendorImportWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}