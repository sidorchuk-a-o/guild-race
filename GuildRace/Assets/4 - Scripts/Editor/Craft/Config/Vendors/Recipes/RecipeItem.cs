using AD.ToolsCollection;

namespace Game.Craft
{
    /// <summary>
    /// Item: <see cref="RecipeData"/>
    /// </summary>
    public class RecipeItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => CraftEditorState.EditorsFactory;
    }
}