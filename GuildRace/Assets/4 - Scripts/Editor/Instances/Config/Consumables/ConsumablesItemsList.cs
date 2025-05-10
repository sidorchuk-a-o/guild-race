using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Game.Instances
{
    public class ConsumablesItemsList : ListElement<ConsumablesItemData, ConsumablesItemItem>
    {
        protected override List<Header> Headers => new()
        {
            new Header("Icon", 68, LengthUnit.Pixel),
            new Header("Id", 70, LengthUnit.Pixel),
            new Header("Title"),
            new Header("Mech", 200, LengthUnit.Pixel),
            new Header("Mech Params", 200, LengthUnit.Pixel),
            new Header("Type", 127, LengthUnit.Pixel),
            new Header("Rarity", 127, LengthUnit.Pixel),
            new Header("Stack", 80, LengthUnit.Pixel)
        };

        public override void BindData(SerializedData data)
        {
            showCloneButton = false;
            showRemoveButton = false;

            wizardType = typeof(ConsumablesItemImportWizard);

            base.BindData(data);
        }
    }
}