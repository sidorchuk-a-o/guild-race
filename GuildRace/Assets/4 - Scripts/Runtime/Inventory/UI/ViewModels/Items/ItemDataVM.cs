using AD.Services.Localization;
using AD.Services.Router;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Inventory
{
    public abstract class ItemDataVM : ViewModel
    {
        private readonly InventoryVMFactory inventoryVMF;

        public int Id { get; }
        public LocalizeKey NameKey { get; }
        public ItemType ItemType { get; }
        public AssetReference IconRef { get; }

        public ItemDataVM(ItemData data, InventoryVMFactory inventoryVMF)
        {
            this.inventoryVMF = inventoryVMF;

            Id = data.Id;
            NameKey = data.NameKey;
            ItemType = data.ItemType;
            IconRef = data.IconRef;
        }

        protected override void InitSubscribes()
        {
        }

        // == Icon ==

        public async UniTask<Sprite> LoadIcon(CancellationTokenSource ct)
        {
            return await inventoryVMF.RentImage(IconRef, ct.Token);
        }
    }
}