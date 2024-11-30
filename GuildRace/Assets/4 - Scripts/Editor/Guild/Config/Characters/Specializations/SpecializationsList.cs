using AD.ToolsCollection;

namespace Game.Guild
{
    public class SpecializationsList : ListElement<SpecializationData, SpecializationItem>
    {
        public override void BindData(SerializedData data)
        {
            wizardType = typeof(SpecializationCreateWizard);

            showCloneButton = false;

            base.BindData(data);
        }
    }
}