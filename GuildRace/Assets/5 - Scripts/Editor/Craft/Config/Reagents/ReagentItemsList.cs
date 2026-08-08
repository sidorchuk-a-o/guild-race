using System;
using System.Collections.Generic;
using AD.ToolsCollection;

namespace AD.Game.Craft
{
    public class ReagentItemsList : ListElement<ReagentItemData>
    {
        protected override Type WizardType { get; } = typeof(ReagentItemImportWizard);
        protected override Type ItemType { get; } = typeof(ReagentItemItem);

        protected override List<Header> Headers => new()
        {
            new("Icon", 68),
            new("Id", 70),
            new("Title"),
            new("Rarity", 127),
            new("Stack", 70)
        };

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}