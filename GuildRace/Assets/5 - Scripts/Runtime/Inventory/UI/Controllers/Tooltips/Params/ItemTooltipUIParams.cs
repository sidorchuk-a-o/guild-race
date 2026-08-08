using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AD.Game.Inventory
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