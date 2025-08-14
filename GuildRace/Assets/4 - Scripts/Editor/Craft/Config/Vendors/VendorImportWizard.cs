using AD.Services.Localization;
using AD.Services.Store;
using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;

namespace Game.Craft
{
    [CreateWizard(typeof(VendorData))]
    public class VendorImportWizard : EntitiesImportWizard<int>
    {
        private GoogleSheetsImporter recipeImporter;

        public override string IdKey => "ID";
        public override string TitleKey => "Name";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "craft-data";
        public override string SheetRange => "A2:F";

        protected override async void SaveCallback()
        {
            LockWizard();

            recipeImporter ??= new(SheetId, "craft", "A:Q", typeof(RecipeData));

            await recipeImporter.LoadData("ID");

            base.SaveCallback();
        }

        protected override void UpdateData(SerializedData vendorData, IDataRow row)
        {
            base.UpdateData(vendorData, row);

            var nameKey = row["Name Key"].LocalizeKeyParse();
            var descKey = row["Desc Key"].LocalizeKeyParse();
            var iconRef = row["Icon Name"].AddressableFileParse();

            vendorData.GetProperty("nameKey").SetValue(nameKey);
            vendorData.GetProperty("descKey").SetValue(descKey);
            vendorData.GetProperty("iconRef").SetValue(iconRef);

            ImportRecipes(vendorData);
        }

        private void ImportRecipes(SerializedData vendorData)
        {
            var vendorId = vendorData.GetProperty("id").GetValue<int>();

            var recipesData = vendorData.GetProperty("recipes");
            var recipesSaveMeta = new SaveMeta(isSubObject: true, recipesData);

            recipeImporter.ImportData(recipesSaveMeta, CheckRecipeEqual, UpdateRecipeData, onFilterRow: row =>
            {
                var id = row["Crafter ID"].IntParse();

                return vendorId == id;
            });
        }

        private bool CheckRecipeEqual(SerializedData data, IDataRow row)
        {
            var dataId = data.GetProperty("id").GetValue<int>();
            var rowId = row["ID"].IntParse();

            return dataId == rowId;
        }

        private void UpdateRecipeData(SerializedData recipeData, IDataRow row)
        {
            var id = row["ID"].IntParse();
            var title = row["Item"].ToUpperFirst();
            var productItemId = row["Item ID"].IntParse();

            recipeData.GetProperty("id").SetValue(id);
            recipeData.GetProperty("title").SetValue(title);
            recipeData.GetProperty("productItemId").SetValue(productItemId);

            // reagents
            var reagents = new List<(int reagentId, int count)>
            {
                (row["Reagent 1 ID"].IntParse(), row["Amount 1"].IntParse()),
                (row["Reagent 2 ID"].IntParse(), row["Amount 2"].IntParse()),
                (row["Reagent 3 ID"].IntParse(), row["Amount 3"].IntParse())
            };

            var ingredients = reagents
                .Where(x => x.reagentId > 0)
                .Select(x => new IngredientData(x.reagentId, x.count))
                .ToList();

            recipeData.GetProperty("ingredients").SetValue(ingredients);

            // price
            var cost = row["Cost"].IntParse();
            var currencyKey = CurrencyKeys.Gold;
            var price = new CurrencyAmount(currencyKey, cost);

            recipeData.GetProperty("price").SetValue((CurrencyAmountData)price);
        }
    }
}