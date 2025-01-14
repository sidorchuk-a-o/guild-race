using System;
using UnityEngine;

namespace Game.Craft
{
    [Serializable]
    public class ProductData
    {
        [SerializeField] private int itemId;
        [SerializeField] private int count;

        public int ItemId => itemId;
        public int Count => count;
    }
}