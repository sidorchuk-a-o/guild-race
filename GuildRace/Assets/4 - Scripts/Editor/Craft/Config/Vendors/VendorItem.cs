using AD.ToolsCollection;

namespace Game.Craft
{
    /// <summary>
    /// Item: <see cref="VendorData"/>
    /// </summary>
    public class VendorItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => CraftEditorState.EditorsFactory;
    }
}