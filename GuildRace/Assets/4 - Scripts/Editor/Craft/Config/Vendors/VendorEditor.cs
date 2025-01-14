using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Craft
{
    [CraftEditor(typeof(VendorData))]
    public class VendorEditor : EntityEditor
    {
        private RecipesList recipesList;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Recipes", CreateRecipesTab);
            tabs.content.FlexWidth(50);
        }

        private void CreateRecipesTab(VisualElement root, SerializedData data)
        {
            recipesList = root.CreateElement<RecipesList>();
            recipesList.BindProperty("recipes", data);
        }
    }
}