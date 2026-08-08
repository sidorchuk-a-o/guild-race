using System;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class RewardMechanicUIParamsList : ListElement<RewardMechanicUIParams>
    {
        protected override Type ItemType { get; } = typeof(RewardMechanicUIParamsItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            Reorderable = false;
            ShowAddButton = false;
            ShowCloneButton = false;
            ShowRemoveButton = false;

            UpdateList();
        }

        private void UpdateList()
        {
            var mechanicIds = InstancesEditorState.CreateRewardMechanicsCollection();

            CreateOrUpdateList(mechanicIds, getFilterId, initData);

            return;
            int getFilterId(ListItem<RewardMechanicUIParams> item)
            {
                return item.Value.MechanicId;
            }

            void initData(RewardMechanicUIParams data, int id)
            {
                data.SetValue("mechanicId", id);
            }
        }
    }
}