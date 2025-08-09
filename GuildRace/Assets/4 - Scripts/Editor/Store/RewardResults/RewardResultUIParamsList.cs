using System.Collections.Generic;
using System.Linq;
using AD.Services.Store;
using AD.ToolsCollection;

namespace Game.Store
{
    public class RewardResultUIParamsList : ListElement<RewardResultUIParams, RewardResultUIParamsItem>
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
            var list = data.GetValue<List<RewardResultUIParams>>(new());
            var itemTypes = ReflectionEditorUtils.GetTypesDerivedFrom<RewardData>();

            // remove old items
            var removedItems = list
                .Where(x => !itemTypes.Any(t => t == x.RewardType))
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
                .Where(x => !list.Any(t => t.RewardType == x))
                .ToListPool();

            if (newItemTypes.Any())
            {
                var saveMeta = new SaveMeta(isSubObject, data);

                foreach (var rewardType in newItemTypes)
                {
                    var newItem = DataFactory.Create(typeof(RewardResultUIParams), saveMeta);

                    newItem.SetValue("rewardType", rewardType.AssemblyQualifiedName);
                }

                data.MarkAsDirty();
            }

            removedItems.ReleaseListPool();
            newItemTypes.ReleaseListPool();
        }
    }
}