using AD.Services.Store;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Craft
{
    [CraftEditor(typeof(RecipeData))]
    public class RecipeEditor : EntityEditor
    {
        private CurrencyAmountElement amountField;
        private IngredientsList ingredientsList;
        private ProductsList productsList;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Params", CreateParamsTab);
            tabs.content.FlexWidth(50);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("Ingredients");

            amountField = root.CreateElement<CurrencyAmountElement>();
            amountField.BindProperty("amount", data);

            ingredientsList = root.CreateElement<IngredientsList>();
            ingredientsList.BindProperty("ingredients", data);

            root.CreateHeader("Products");

            productsList = root.CreateElement<ProductsList>();
            productsList.BindProperty("products", data);
        }
    }
}