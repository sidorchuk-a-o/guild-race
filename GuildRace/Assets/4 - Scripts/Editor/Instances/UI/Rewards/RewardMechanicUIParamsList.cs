using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;

namespace Game.Instances
{
    public class RewardMechanicUIParamsList : ListElement<RewardMechanicUIParams, RewardMechanicUIParamsItem>
    {
        public override void BindData(SerializedData data)
        {
            UpdateList(data);

            reorderable = false;
            showAddButton = false;
            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }

        private void UpdateList(SerializedData data)
        {
            var list = data.GetValue<List<RewardMechanicUIParams>>(new());
            var itemTypes = InstancesEditorState.CreateRewardMechanicsCollection();

            // remove old items
            var removedItems = list
                .Where(x => !itemTypes.Any(t => t.value == x.MechanicId))
                .ToListPool();

            if (removedItems.Any())
            {
                var saveMeta = new SaveMeta(isSubObject, data);

                foreach (var item in removedItems)
                {
                    DataFactory.Remove(item, saveMeta);
                }

                data.MarkAsDirty();
            }

            // create new items
            var newItemTypes = itemTypes
                .Where(x => !list.Any(t => t.MechanicId == x.value))
                .ToListPool();

            if (newItemTypes.Any())
            {
                var saveMeta = new SaveMeta(isSubObject, data);

                foreach (var (option, value) in newItemTypes)
                {
                    var newItem = DataFactory.Create(typeof(RewardMechanicUIParams), saveMeta);

                    newItem.SetValue("mechanicId", value);
                }

                data.MarkAsDirty();
            }

            removedItems.ReleaseListPool();
            newItemTypes.ReleaseListPool();
        }
    }
}