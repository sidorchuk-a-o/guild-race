using System;
using UnityEngine;

namespace AD.Game.Inventory
{
    [Serializable]
    public struct ItemStack
    {
        public static ItemStack Default = new(1);

        [SerializeField] private int size;

        public readonly int Size => size;

        public ItemStack(int size)
        {
            this.size = size;
        }
    }
}