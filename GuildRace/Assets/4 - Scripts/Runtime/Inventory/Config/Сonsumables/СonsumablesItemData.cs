using UnityEngine;

namespace Game.Inventory
{
    public class СonsumablesItemData : ItemData
    {
        [SerializeField] private ItemStack stack;

        public ItemStack Stack => stack;
    }
}