using AD.ToolsCollection;

namespace Game.Guild
{
    public class DefaultCharactersList : ListElement<DefaultCharacterData, DefaultCharacterItem>
    {
        public override void BindData(SerializedData data)
        {
            showCloneButton = false;

            wizardType = typeof(DefaultCharacterCreateWizard);

            base.BindData(data);
        }
    }
}