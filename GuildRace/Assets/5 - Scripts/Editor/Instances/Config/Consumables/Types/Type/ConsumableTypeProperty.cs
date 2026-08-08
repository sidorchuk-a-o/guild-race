using System;
using AD.ToolsCollection;
using UnityEditor;

namespace AD.Game.Instances
{
    [CustomPropertyDrawer(typeof(ConsumableType))]
    public class ConsumableTypeProperty : KeyPropertyDrawer<ConsumableType, int>
    {
    }

    [KeyEditor(typeof(ConsumableType))]
    public class ConsumableTypeElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InstancesEditorState.GetConsumableTypesViewCollection;
        }
    }
}