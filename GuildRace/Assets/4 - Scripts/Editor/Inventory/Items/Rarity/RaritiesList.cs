using AD.ToolsCollection;

namespace Game.Inventory
{
    public class RaritiesList : ListElement<RarityData, RarityItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Rarities";
            wizardType = typeof(RarityCreateWizard);

            showCloneButton = false;

            base.BindData(data);
        }
    }
}