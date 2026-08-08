using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AD.Game.Inventory
{
    [Serializable]
    public class ItemGridUIParams
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private AssetReference itemInGridRef;

        public ItemType ItemType => itemType;
        public AssetReference ItemInGridRef => itemInGridRef;
    }
}