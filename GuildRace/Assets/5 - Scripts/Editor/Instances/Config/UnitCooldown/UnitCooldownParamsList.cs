using System;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class UnitCooldownParamsList : ListElement<UnitCooldownParams>
    {
        protected override Type ItemType { get; } = typeof(UnitCooldownParamsItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Unit Cooldown Params";

            ShowCloneButton = false;
        }
    }
}