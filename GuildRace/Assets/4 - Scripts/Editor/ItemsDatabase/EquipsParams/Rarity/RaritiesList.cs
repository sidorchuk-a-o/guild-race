using AD.ToolsCollection;

namespace Game.Items
{
    public class RaritiesList : ListElement<RarityData, RarityItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Rarity";
            wizardType = typeof(RarityCreateWizard);

            showCloneButton = false;

            base.BindData(data);
        }
    }
}