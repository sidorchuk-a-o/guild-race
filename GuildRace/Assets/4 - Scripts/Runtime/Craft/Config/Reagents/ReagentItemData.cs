using Game.Inventory;
using UnityEngine;

namespace Game.Craft
{
    public class ReagentItemData : ItemData
    {
        [SerializeField] private ItemStack stack;

        public ItemStack Stack => stack;
    }
}