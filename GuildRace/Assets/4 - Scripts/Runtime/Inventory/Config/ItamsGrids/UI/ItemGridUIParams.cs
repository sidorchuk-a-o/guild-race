using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Inventory
{
    [Serializable]
    public class ItemGridUIParams
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private AssetReference itemInGridRef;
        [SerializeField] private AssetReference itemTooltipRef;

        public ItemType ItemType => itemType;
        public AssetReference ItemInGridRef => itemInGridRef;
        public AssetReference ItemTooltipRef => itemTooltipRef;
    }
}