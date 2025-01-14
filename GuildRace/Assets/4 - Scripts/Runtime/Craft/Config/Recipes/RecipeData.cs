using AD.Services.Store;
using AD.ToolsCollection;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Craft
{
    [Serializable]
    public class RecipeData : Entity<int>
    {
        [SerializeField] private CurrencyAmountData amount = new();
        [SerializeField] private List<IngredientData> ingredients;
        [Space]
        [SerializeField] private List<ProductData> products;

        public CurrencyAmount Amount => amount;
        public IReadOnlyList<IngredientData> Ingredients => ingredients;
        public IReadOnlyList<ProductData> Products => products;
    }
}