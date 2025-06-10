using UnityEngine;
using UnityEngine.AddressableAssets;
using System;

namespace Game.Inventory
{
    [Serializable]
    public class ItemTooltipUIParams
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private AssetReference tooltipRef;

        public ItemType ItemType => itemType;
        public AssetReference TooltipRef => tooltipRef;
    }
}