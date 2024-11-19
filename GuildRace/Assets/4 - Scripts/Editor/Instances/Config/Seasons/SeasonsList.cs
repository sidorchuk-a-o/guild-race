using AD.ToolsCollection;

namespace Game.Instances
{
    public class SeasonsList : ListElement<SeasonData, SeasonItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Seasons";
            wizardType = typeof(SeasonsImportWizard);

            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}