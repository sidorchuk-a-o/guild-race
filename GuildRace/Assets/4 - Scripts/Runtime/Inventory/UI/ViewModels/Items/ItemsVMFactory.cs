using AD.ToolsCollection;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Inventory
{
    public abstract class ItemsVMFactory : ScriptableData
    {
        [SerializeField] private AssetReference itemInGridRef;

        public abstract Type InfoType { get; }

        public AssetReference ItemInGridRef => itemInGridRef;

        public abstract ItemVM Create(ItemInfo info, InventoryVMFactory inventoryVMF);
    }
}