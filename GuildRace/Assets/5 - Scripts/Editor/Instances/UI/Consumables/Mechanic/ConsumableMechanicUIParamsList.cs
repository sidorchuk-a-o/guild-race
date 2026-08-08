using System;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class ConsumableMechanicUIParamsList : ListElement<ConsumableMechanicUIParams>
    {
        protected override Type ItemType { get; } = typeof(ConsumableMechanicUIParamsItem);

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
            var mechanicIds = InstancesEditorState.CreateConsumableMechanicsCollection();

            CreateOrUpdateList(mechanicIds, getFilterId, initData);

            return;
            int getFilterId(ListItem<ConsumableMechanicUIParams> item)
            {
                return item.Value.MechanicId;
            }

            void initData(ConsumableMechanicUIParams data, int id)
            {
                data.SetValue("mechanicId", id);
            }
        }
    }
}