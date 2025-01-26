using AD.ToolsCollection;

namespace Game.Quests
{
    public class QuestsGroupModulesList : ListElement<QuestsGroupModule, QuestsGroupModuleItem>
    {
        public override void BindData(SerializedData data)
        {
            wizardType = typeof(QuestsGroupModuleImportWizard);

            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}