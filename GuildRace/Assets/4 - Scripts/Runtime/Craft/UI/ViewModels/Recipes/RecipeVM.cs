using System.Collections.Generic;
using System.Linq;
using AD.Services.Router;
using AD.ToolsCollection;
using Game.Inventory;
using UniRx;
using UnityEngine;

namespace Game.Craft
{
    public class RecipeVM : ViewModel
    {
        private readonly ReactiveProperty<int> availableCount = new();
        private readonly ReactiveProperty<string> availableCountStr = new();

        public int Id { get; }

        public ItemDataVM ProductVM { get; }
        public IReadOnlyList<IngredientVM> IngridientsVM { get; }

        public IReadOnlyReactiveProperty<int> AvailableCount => availableCount;
        public IReadOnlyReactiveProperty<string> AvailableCountStr => availableCountStr;

        public RecipeVM(RecipeData data, CraftVMFactory craftVMF)
        {
            Id = data.Id;
            ProductVM = craftVMF.InventoryVMF.CreateItemData(data.ProductItemId);
            IngridientsVM = craftVMF.GetRecipeIngredients(Id);
        }

        protected override void InitSubscribes()
        {
            ProductVM.AddTo(this);
            IngridientsVM.ForEach(x => x.AddTo(this));

            foreach (var ingredientVM in IngridientsVM)
            {
                ingredientVM.ReagentCounterVM.Count
                    .SilentSubscribe(UpdateAvailableCount)
                    .AddTo(this);
            }

            UpdateAvailableCount();
        }

        private void UpdateAvailableCount()
        {
            var maxCount = IngridientsVM.Min(ingridient =>
            {
                var ingridientCount = ingridient.Count;
                var reagentCount = ingridient.ReagentCounterVM.Count.Value;

                return Mathf.CeilToInt(reagentCount / ingridientCount);
            });

            availableCount.Value = maxCount;
            availableCountStr.Value = maxCount > 0 ? maxCount.ToString() : string.Empty;
        }
    }
}