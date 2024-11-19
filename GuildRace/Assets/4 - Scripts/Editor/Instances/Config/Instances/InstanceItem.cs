using AD.ToolsCollection;

namespace Game.Instances
{
    /// <summary>
    /// Item: <see cref="InstanceData"/>
    /// </summary>
    public class InstanceItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => InstancesEditorState.EditorsFactory;
    }
}