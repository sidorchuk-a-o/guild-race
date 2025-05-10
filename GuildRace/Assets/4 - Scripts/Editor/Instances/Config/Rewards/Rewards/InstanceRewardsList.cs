using System.Collections.Generic;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    public class InstanceRewardsList : ListElement<InstanceRewardData, InstanceRewardItem>
    {
        protected override List<Header> Headers => new()
        {
            new Header("Id", 70, LengthUnit.Pixel),
            new Header("Boss", 200, LengthUnit.Pixel),
            new Header("Mech", 120, LengthUnit.Pixel),
            new Header("Mech Params")
        };

        public override void BindData(SerializedData data)
        {
            wizardType = typeof(InstanceRewardImportWizard);

            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}