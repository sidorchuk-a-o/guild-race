using AD.ToolsCollection;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Inventory
{
    public abstract class ItemsVMFactory : ScriptableData
    {
        [SerializeField] private AssetReference itemInGridRef;
        [SerializeField] private AssetReference itemInSlotRef;

        public abstract Type InfoType { get; }

        public AssetReference ItemInGridRef => itemInGridRef;
        public AssetReference ItemInSlotRef => itemInSlotRef;

        public abstract ItemVM Create(ItemInfo itemInfo, InventoryVMFactory inventoryVMF);
    }
}