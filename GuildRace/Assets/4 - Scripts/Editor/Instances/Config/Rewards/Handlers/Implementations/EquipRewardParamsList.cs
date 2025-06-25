using AD.ToolsCollection;

namespace Game.Instances
{
    public class EquipRewardParamsList : ListElement<EquipRewardParams, EquipRewardParamsItem>
    {
        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            showAddButton = false;
            showCloneButton = false;
            showRemoveButton = false;
        }
    }
}