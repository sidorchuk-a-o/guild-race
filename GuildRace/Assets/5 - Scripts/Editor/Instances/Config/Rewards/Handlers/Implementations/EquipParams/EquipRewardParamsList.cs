using System;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class EquipRewardParamsList : ListElement<EquipRewardParams>
    {
        protected override Type ItemType { get; } = typeof(EquipRewardParamsItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowAddButton = false;
            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}