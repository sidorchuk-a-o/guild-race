using AD.ToolsCollection;

namespace Game.Craft
{
    public class IngredientsList : ListElement<IngredientData, IngredientItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Ingredients";

            showAddButton = false;
            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}