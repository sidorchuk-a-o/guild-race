using AD.Services.Localization;
using AD.ToolsCollection;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Craft
{
    [Serializable]
    public class VendorData : Entity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private LocalizeKey descKey;
        [SerializeField] private AssetReference iconRef;
        [Space]
        [SerializeField] private List<RecipeData> recipes;

        public LocalizeKey NameKey => nameKey;
        public LocalizeKey DescKey => descKey;
        public AssetReference IconRef => iconRef;

        public IReadOnlyList<RecipeData> Recipes => recipes;
    }
}