using AD.ToolsCollection;

namespace Game.Craft
{
    public class RecipesList : ListElement<RecipeData, RecipeItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Recipes";

            showAddButton = false;
            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}