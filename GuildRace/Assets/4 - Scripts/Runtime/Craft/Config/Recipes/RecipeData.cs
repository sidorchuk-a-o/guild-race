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
        [SerializeField] private int productItemId;
        [SerializeField] private List<IngredientData> ingredients;
        [SerializeField] private CurrencyAmountData price;

        public int ProductItemId => productItemId;
        public IReadOnlyList<IngredientData> Ingredients => ingredients;
        public CurrencyAmount Price => price;
    }
}