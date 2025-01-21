using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Craft
{
    public class CraftConfig : ScriptableConfig
    {
        [SerializeField] private List<VendorData> vendors;
        [SerializeField] private ReagentsParams reagentsParams = new();
        [SerializeField] private RemoveItemSlotData removeItemSlot;

        private Dictionary<int, VendorData> vendorsCache;
        private Dictionary<int, RecipeData> recipesCache;

        public IReadOnlyList<VendorData> Vendors => vendors;
        public ReagentsParams ReagentsParams => reagentsParams;
        public RemoveItemSlotData RemoveItemSlot => removeItemSlot;

        public VendorData GetVendor(int id)
        {
            vendorsCache ??= vendors.ToDictionary(x => x.Id, x => x);
            vendorsCache.TryGetValue(id, out var data);

            return data;
        }

        public RecipeData GetRecipe(int id)
        {
            recipesCache ??= vendors.SelectMany(x => x.Recipes).ToDictionary(x => x.Id, x => x);
            recipesCache.TryGetValue(id, out var data);

            return data;
        }
    }
}