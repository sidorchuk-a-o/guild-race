using AD.ToolsCollection;

namespace Game.Instances
{
    /// <summary>
    /// Item: <see cref="SeasonData"/>
    /// </summary>
    public class SeasonItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => InstancesEditorState.EditorsFactory;
    }
}