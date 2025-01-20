using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Game.Instances
{
    public class СonsumablesItemsList : ListElement<СonsumablesItemData, СonsumablesItemItem>
    {
        protected override List<Header> Headers => new()
        {
            new Header("Icon", 68, LengthUnit.Pixel),
            new Header("Id", 70, LengthUnit.Pixel),
            new Header("Title"),
            new Header("Rarity", 127, LengthUnit.Pixel)
        };

        public override void BindData(SerializedData data)
        {
            showCloneButton = false;
            showRemoveButton = false;

            wizardType = typeof(СonsumablesItemImportWizard);

            base.BindData(data);
        }
    }
}