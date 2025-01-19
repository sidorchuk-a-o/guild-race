using UnityEngine;

namespace Game.Inventory
{
    public class СonsumablesItemData : ItemData, IStackable
    {
        [SerializeField] private ItemStack stack;

        public ItemStack Stack => stack;
    }
}