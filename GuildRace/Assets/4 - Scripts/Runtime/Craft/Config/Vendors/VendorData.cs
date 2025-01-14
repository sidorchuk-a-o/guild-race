using AD.ToolsCollection;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Craft
{
    [Serializable]
    public class VendorData : Entity<int>
    {
        [SerializeField] private List<RecipeData> recipes;

        public IReadOnlyList<RecipeData> Recipes => recipes;
    }
}