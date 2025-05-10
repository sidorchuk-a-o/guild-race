using AD.ToolsCollection;

namespace Game.Instances
{
    public class ThreatsList : ListElement<ThreatData, ThreatItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Threats";
            wizardType = typeof(ThreatDataImportWizard);

            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}