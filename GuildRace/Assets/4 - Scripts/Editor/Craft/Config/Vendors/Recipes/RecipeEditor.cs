using AD.Services.Store;
using AD.ToolsCollection;
using Game.Inventory;
using UnityEngine.UIElements;

namespace Game.Craft
{
    [CraftEditor(typeof(RecipeData))]
    public class RecipeEditor : EntityEditor
    {
        private PopupElement<int> productItemPopup;
        private IngredientsList ingredientsList;
        private CurrencyAmountElement priceField;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Params", CreateParamsTab);
            tabs.content.FlexWidth(50);
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