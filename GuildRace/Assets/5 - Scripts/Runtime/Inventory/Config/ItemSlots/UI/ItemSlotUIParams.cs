using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AD.Game.Inventory
{
    [Serializable]
    public class ItemSlotUIParams
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private AssetReference itemInSlotRef;

        public ItemType ItemType => itemType;
        public AssetReference ItemInSlotRef => itemInSlotRef;
    }
}