using AD.ToolsCollection;
using System;

namespace Game.Instances
{
    public class InstancesEditorAttribute : EditorAttribute
    {
        public InstancesEditorAttribute(Type dataType) : base(dataType)
        {
        }
    }
}