﻿using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Game.Inventory
{
    public class ItemsFactoriesList : ListElement<ItemsFactory, ItemsFactoryItem>
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
            var list = data.GetValue<List<ItemsFactory>>(new());
            var types = ReflectionEditorUtils.GetTypesDerivedFrom<ItemsFactory>();

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
                    var factory = DataFactory.Create(type, saveMeta);

                    var title = type.Name
                        .Clear("Factory")
                        .SplitByUpper();

                    factory.SetValue("title", title);
                }

                data.MarkAsDirty();
            }

            removedFactories.ReleaseListPool();
            newTypes.ReleaseListPool();
        }
    }
}