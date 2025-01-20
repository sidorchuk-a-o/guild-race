using AD.Services.Localization;
using Game.Inventory;
using UnityEngine;

namespace Game.Instances
{
    public class СonsumablesItemData : ItemData, IStackable
    {
        // Params
        [SerializeField] private Rarity rarity;
        [SerializeField] private ItemStack stack;
        // View
        [SerializeField] private LocalizeKey descKey;

        public Rarity Rarity => rarity;
        public ItemStack Stack => stack;
        public LocalizeKey DescKey => descKey;
    }
}