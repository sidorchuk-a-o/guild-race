using System;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    public class DefaultCharactersList : ListElement<DefaultCharacterData>
    {
        protected override Type WizardType { get; } = typeof(DefaultCharacterImportWizard);
        protected override Type ItemType { get; } = typeof(DefaultCharacterItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}