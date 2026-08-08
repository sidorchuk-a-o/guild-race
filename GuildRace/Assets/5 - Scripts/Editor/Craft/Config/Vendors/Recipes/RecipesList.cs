using AD.ToolsCollection;

namespace AD.Game.Craft
{
    public class RecipesList : ListElement<RecipeData>
    {
        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowAddButton = false;
            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}