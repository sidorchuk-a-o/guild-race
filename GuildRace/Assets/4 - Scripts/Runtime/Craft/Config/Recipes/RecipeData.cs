using AD.Services.Localization;
using AD.ToolsCollection;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Craft
{
    [Serializable]
    public class RecipeData : Entity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [Space]
        [SerializeField] private int productItemId;
        [SerializeField] private List<IngredientData> ingredients;

        public LocalizeKey NameKey => nameKey;
        public int ProductItemId => productItemId;
        public IReadOnlyList<IngredientData> Ingredients => ingredients;
    }
}