using System;
using AD.ToolsCollection;

namespace AD.Game.Quests
{
    public class QuestMechanicHandlersList : ListElement<QuestMechanicHandler>
    {
        protected override Type ItemType { get; } = typeof(QuestMechanicHandlerItem);
        protected override Type WizardType { get; } = typeof(QuestMechanicHandlerImportWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}