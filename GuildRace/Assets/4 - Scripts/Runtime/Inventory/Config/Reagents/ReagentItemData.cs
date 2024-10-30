using UnityEngine;

namespace Game.Inventory
{
    public class ReagentItemData : ItemData
    {
        [SerializeField] private ItemStack stack;

        public ItemStack Stack => stack;
    }
}