using System;
using AD.ToolsCollection;

namespace AD.Game.Quests
{
    public class QuestsGroupModulesList : ListElement<QuestsGroupModule>
    {
        protected override Type ItemType { get; } = typeof(QuestsGroupModuleItem);
        protected override Type WizardType { get; } = typeof(QuestsGroupModuleImportWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}