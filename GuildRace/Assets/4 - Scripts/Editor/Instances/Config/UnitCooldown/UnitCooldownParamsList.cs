using AD.ToolsCollection;

namespace Game.Instances
{
    public class UnitCooldownParamsList : ListElement<UnitCooldownParams, UnitCooldownParamsItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Unit Cooldown Params";

            showCloneButton = false;

            base.BindData(data);
        }
    }
}