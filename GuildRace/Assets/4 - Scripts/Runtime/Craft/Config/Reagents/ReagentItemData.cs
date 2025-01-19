using Game.Inventory;
using UnityEngine;

namespace Game.Craft
{
    public class ReagentItemData : ItemData, IStackable
    {
        [SerializeField] private ItemStack stack;

        public ItemStack Stack => stack;
    }
}