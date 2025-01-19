using AD.Services.Localization;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Inventory
{
    public class ItemDataVM : ViewModel
    {
        private readonly ItemData data;
        private readonly InventoryVMFactory inventoryVMF;

        private Sprite sprite;

        public int Id { get; }
        public LocalizeKey NameKey { get; }

        public ItemDataVM(ItemData data, InventoryVMFactory inventoryVMF)
        {
            this.data = data;
            this.inventoryVMF = inventoryVMF;

            Id = data.Id;
            NameKey = data.NameKey;
        }

        protected override void InitSubscribes()
        {
        }

        // == Icon ==

        public async UniTask<Sprite> LoadIcon()
        {
            if (sprite == null)
            {
                sprite = await inventoryVMF.RentSprite(data.IconRef);
            }

            return sprite;
        }
    }
}