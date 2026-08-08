using System;
using System.Collections.Generic;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class ConsumablesItemsList : ListElement<ConsumablesItemData>
    {
        protected override Type ItemType { get; } = typeof(ConsumablesItemItem);
        protected override Type WizardType { get; } = typeof(ConsumablesItemImportWizard);

        protected override List<Header> Headers => new()
        {
            new("Icon", 68),
            new("Id", 70),
            new("Title"),
            new("Mech", 200),
            new("Mech Params", 200),
            new("Type", 127),
            new("Rarity", 127),
            new("Stack", 80)
        };

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}