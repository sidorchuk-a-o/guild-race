using System;
using AD.ToolsCollection;

namespace AD.Game.Quests
{
    public class QuestsList : ListElement<QuestData>
    {
        protected override Type ItemType { get; } = typeof(QuestItem);
        protected override Type WizardType { get; } = typeof(QuestImportWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}