using System;
using AD.ToolsCollection;
using UnityEditor;

namespace AD.Game.Instances
{
    [CustomPropertyDrawer(typeof(InstanceType))]
    public class InstanceTypeProperty : KeyPropertyDrawer<InstanceType, int>
    {
    }

    [KeyEditor(typeof(InstanceType))]
    public class InstanceTypeElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InstancesEditorState.GetInstanceTypesViewCollection;
        }
    }
}