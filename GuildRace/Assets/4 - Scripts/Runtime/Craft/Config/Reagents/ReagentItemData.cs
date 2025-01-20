using Game.Inventory;
using UnityEngine;

namespace Game.Craft
{
    public class ReagentItemData : ItemData, IStackable
    {
        [SerializeField] private ItemStack stack;
        [SerializeField] private Rarity rarity;

        public ItemStack Stack => stack;
        public Rarity Rarity => rarity;
    }
}