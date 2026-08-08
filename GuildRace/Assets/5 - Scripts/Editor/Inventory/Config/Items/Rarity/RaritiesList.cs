using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class RaritiesList : ListElement<RarityData>
    {
        protected override Type ItemType { get; } = typeof(RarityItem);
        protected override Type WizardType { get; } = typeof(RarityImportWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Rarities";
            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}