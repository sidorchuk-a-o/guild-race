using AD.ToolsCollection;

namespace Game.Quests
{
    public class QuestsList : ListElement<QuestData, QuestItem>
    {
        public override void BindData(SerializedData data)
        {
            wizardType = typeof(QuestImportWizard);

            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}