using AD.Services.Localization;
using AD.Services.Router;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Inventory
{
    public abstract class ItemDataVM : ViewModel
    {
        private readonly ItemData data;
        private readonly InventoryVMFactory inventoryVMF;

        public int Id { get; }
        public LocalizeKey NameKey { get; }
        public ItemType ItemType { get; }

        public ItemDataVM(ItemData data, InventoryVMFactory inventoryVMF)
        {
            this.data = data;
            this.inventoryVMF = inventoryVMF;

            Id = data.Id;
            NameKey = data.NameKey;
            ItemType = data.ItemType;
        }

        protected override void InitSubscribes()
        {
        }

        // == Icon ==

        public async UniTask<Sprite> LoadIcon(CancellationTokenSource ct)
        {
            return await inventoryVMF.RentImage(data.IconRef, ct.Token);
        }
    }
}