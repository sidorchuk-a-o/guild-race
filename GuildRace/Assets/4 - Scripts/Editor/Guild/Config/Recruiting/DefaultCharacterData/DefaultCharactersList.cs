using AD.ToolsCollection;

namespace Game.Guild
{
    public class DefaultCharactersList : ListElement<DefaultCharacterData, DefaultCharacterItem>
    {
        public override void BindData(SerializedData data)
        {
            showCloneButton = false;
            showRemoveButton = false;

            wizardType = typeof(DefaultCharacterImportWizard);

            base.BindData(data);
        }
    }
}