using System;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class SeasonsList : ListElement<SeasonData>
    {
        protected override Type ItemType { get; } = typeof(SeasonItem);
        protected override Type WizardType { get; } = typeof(SeasonsImportWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Seasons";

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}