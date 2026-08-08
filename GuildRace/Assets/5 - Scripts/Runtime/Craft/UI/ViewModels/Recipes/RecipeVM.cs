using System.Collections.Generic;
using System.Linq;
using AD.Game.Inventory;
using AD.Router;
using AD.ToolsCollection;
using R3;
using UnityEngine;

namespace AD.Game.Craft
{
    public class RecipeVM : ViewModel
    {
        private readonly ReactiveProperty<int> count = new();
        private readonly ReactiveProperty<int> availableCount = new();
        private readonly ReactiveProperty<string> availableCountStr = new();
        private readonly ReactiveProperty<bool> isAvailable = new();

        public int Id { get; }

        public ItemDataVM ProductVM { get; }
        public IReadOnlyList<IngredientVM> IngredientsVM { get; }
        public CraftPriceVM PriceVM { get; }

        public ReadOnlyReactiveProperty<int> Count => count;
        public ReadOnlyReactiveProperty<bool> IsAvailable => isAvailable;
        public ReadOnlyReactiveProperty<int> AvailableCount => availableCount;
        public ReadOnlyReactiveProperty<string> AvailableCountStr => availableCountStr;

        public RecipeVM(RecipeData data, CraftVMFactory craftVMF)
        {
            Id = data.Id;
            ProductVM = craftVMF.InventoryVMF.CreateItemData(data.ProductItemId);
            IngredientsVM = craftVMF.GetRecipeIngredients(Id);
            PriceVM = new(data, craftVMF);
        }

        public void SetCount(int value)
        {
            count.Value = value;

            PriceVM.UpdatePrice(value);

            // upd ingredients
            foreach (var ingredient in IngredientsVM)
            {
                ingredient.SetCraftingCount(count.Value);
            }

            // check available
            var hasCraft = Count.CurrentValue > 0;
            var ingredientsAvailable = IngredientsVM.All(x => x.IsAvailable.CurrentValue);

            isAvailable.Value = ingredientsAvailable && hasCraft;
        }

        protected override void InitSubscribes()
        {
            ProductVM.AddTo(this);
            IngredientsVM.ForEach(x => x.AddTo(this));
            PriceVM.AddTo(this);

            foreach (var ingredientVM in IngredientsVM)
            {
                ingredientVM.ReagentCounterVM.Count
                    .SilentSubscribe(UpdateAvailableCount)
                    .AddTo(this);
            }

            UpdateAvailableCount();
        }

        private void UpdateAvailableCount()
        {
            var maxCount = IngredientsVM.Min(ingredient =>
            {
                var ingredientCount = ingredient.Count;
                var reagentCount = ingredient.ReagentCounterVM.Count.CurrentValue;

                return Mathf.CeilToInt(reagentCount / (float)ingredientCount);
            });

            availableCount.Value = maxCount;
            availableCountStr.Value = maxCount > 0 ? maxCount.ToString() : string.Empty;
        }
    }
}