using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Game.Craft
{
    public class ReagentItemsList : ListElement<ReagentItemData, ReagentItemItem>
    {
        protected override List<Header> Headers => new()
        {
            new Header("Icon", 68, LengthUnit.Pixel),
            new Header("Id", 70, LengthUnit.Pixel),
            new Header("Title"),
            new Header("Rarity", 127, LengthUnit.Pixel),
            new Header("Stack", 70, LengthUnit.Pixel)
        };

        public override void BindData(SerializedData data)
        {
            showCloneButton = false;
            showRemoveButton = false;

            wizardType = typeof(ReagentItemImportWizard);

            base.BindData(data);
        }
    }
}