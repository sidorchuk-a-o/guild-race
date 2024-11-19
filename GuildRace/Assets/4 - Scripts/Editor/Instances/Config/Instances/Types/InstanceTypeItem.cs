using AD.ToolsCollection;

namespace Game.Instances
{
    /// <summary>
    /// Item: <see cref="InstanceTypeData"/>
    /// </summary>
    public class InstanceTypeItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => InstancesEditorState.EditorsFactory;
    }
}