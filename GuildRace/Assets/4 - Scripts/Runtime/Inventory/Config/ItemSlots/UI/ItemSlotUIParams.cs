using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Inventory
{
    [Serializable]
    public class ItemSlotUIParams
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private AssetReference itemInSlotRef;
        [SerializeField] private AssetReference itemTooltipRef;

        public ItemType ItemType => itemType;
        public AssetReference ItemInSlotRef => itemInSlotRef;
        public AssetReference ItemTooltipRef => itemTooltipRef;
    }
}