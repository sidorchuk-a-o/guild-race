using AD.Game.Inventory;
using AD.Store;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Craft
{
    [DataEditor(typeof(RecipeData))]
    public class RecipeEditor : DataEditor
    {
        private PopupElement<int> productItemPopup;
        private IngredientsList ingredientsList;
        private CurrencyAmountElement priceField;

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

            tabs.CreateTab("Params", CreateParamsTab);
            tabs.Content.FlexWidth(50);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("Product");

            productItemPopup = root.CreatePopup(InventoryEditorState.GetAllItemsCollection);
            productItemPopup.BindProperty("productItemId", data);

            root.CreateHeader("Ingredients");

            ingredientsList = root.CreateElement<IngredientsList>();
            ingredientsList.BindProperty("ingredients", data);

            root.CreateHeader("Price");

            priceField = root.CreateElement<CurrencyAmountElement>();
            priceField.BindProperty("price", data);
        }
    }
}