using System;
using System.Collections.Generic;
using AD.Store;
using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Craft
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