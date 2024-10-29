using System;
using UnityEngine;

namespace Game.Inventory
{
    [Serializable]
    public struct ItemSize
    {
        public static ItemSize Default = new(1, 1);

        [SerializeField] private int width;
        [SerializeField] private int height;

        public readonly int Width => width;
        public readonly int Height => height;

        public ItemSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }
}