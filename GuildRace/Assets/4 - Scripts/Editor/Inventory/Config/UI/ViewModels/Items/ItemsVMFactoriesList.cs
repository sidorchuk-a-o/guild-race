using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;

namespace Game.Inventory
{
    public class ItemsVMFactoriesList : ListElement<ItemsVMFactory, ItemsVMFactoryItem>
    {
        public override void BindData(SerializedData data)
        {
            UpdateFactoriesList(data);

            headerTitle = "Items Factories";

            reorderable = false;
            showAddButton = false;
            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }

        private void UpdateFactoriesList(SerializedData data)
        {
            var list = data.GetValue<List<ItemsVMFactory>>(new());
            var types = ReflectionEditorUtils.GetTypesDerivedFrom<ItemsVMFactory>();

            // remove old factories
            var removedFactories = list
                .Where(x => !types.Any(t => t.Equals(x.GetType())))
                .ToListPool();

            if (removedFactories.Any())
            {
                var saveMeta = new SaveMeta(isSubObject, data);

                foreach (var factory in removedFactories)
                {
                    DataFactory.Remove(factory, saveMeta);
                }

                data.MarkAsDirty();
            }

            // create new factories
            var newTypes = types
                .Where(x => !list.Any(t => t.GetType().Equals(x)))
                .ToListPool();

            if (newTypes.Any())
            {
                var saveMeta = new SaveMeta(isSubObject, data);

                foreach (var type in newTypes)
                {
                    DataFactory.Create(type, saveMeta);
                }

                data.MarkAsDirty();
            }

            removedFactories.ReleaseListPool();
            newTypes.ReleaseListPool();
        }
    }
}