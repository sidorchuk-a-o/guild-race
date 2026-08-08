using System;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    public class ClassesList : ListElement<ClassData>
    {
        protected override Type WizardType { get; } = typeof(ClassImportWizard);
        protected override Type ItemType { get; } = typeof(ClassItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}