using AD.ToolsCollection;

namespace Game.Craft
{
    public class RecipesList : ListElement<RecipeData, RecipeItem>
    {
        public override void BindData(SerializedData data)
        {
            showAddButton = false;
            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}