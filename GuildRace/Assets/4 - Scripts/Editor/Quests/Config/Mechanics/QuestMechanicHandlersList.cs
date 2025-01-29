using AD.ToolsCollection;

namespace Game.Quests
{
    public class QuestMechanicHandlersList : ListElement<QuestMechanicHandler, QuestMechanicHandlerItem>
    {
        public override void BindData(SerializedData data)
        {
            wizardType = typeof(QuestMechanicHandlerImportWizard);

            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}