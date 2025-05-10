using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Craft
{
    [CraftEditor(typeof(VendorData))]
    public class VendorEditor : EntityEditor
    {
        private PropertyElement nameKeyField;
        private PropertyElement descKeyField;
        private AddressableElement<Sprite> iconRefField;
        private RecipesList recipesList;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Params", CreateRecipesTab);
            tabs.content.FlexWidth(50);
        }

        private void CreateRecipesTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("View");

            nameKeyField = root.CreateProperty();
            nameKeyField.BindProperty("nameKey", data);

            descKeyField = root.CreateProperty();
            descKeyField.BindProperty("descKey", data);

            iconRefField = root.CreateAddressable<Sprite>();
            iconRefField.BindProperty("iconRef", data);

            root.CreateHeader("Recipes");

            recipesList = root.CreateElement<RecipesList>();
            recipesList.BindProperty("recipes", data);
        }
    }
}