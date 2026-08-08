using System;
using AD.ToolsCollection;

namespace AD.Game.Store
{
    public class RewardResultUIParamsList : ListElement<RewardResultUIParams>
    {
        protected override Type ItemType { get; } = typeof(RewardResultUIParamsItem);

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
            CreateOrUpdateList(getFilterType, initItem);

            return;
            Type getFilterType(ListItem<RewardResultUIParams> item)
            {
                return item.Value.RewardType;
            }

            void initItem(RewardResultUIParams data, Type type)
            {
                data.SetValue("rewardType", type.AssemblyQualifiedName);
            }
        }
    }
}