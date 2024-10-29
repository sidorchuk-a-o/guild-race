using AD.Services.Localization;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using Game.Inventory;
using UnityEngine;

namespace Game.Guild
{
    public class GuildBankTabVM : ViewModel
    {
        private readonly GuildBankTabInfo info;
        private readonly InventoryVMFactory inventoryVMF;

        public string Id { get; }

        public LocalizeKey NameKey { get; }
        public ItemsGridVM GridVM { get; }

        public GuildBankTabVM(GuildBankTabInfo info, InventoryVMFactory inventoryVMF)
        {
            this.info = info;
            this.inventoryVMF = inventoryVMF;

            Id = info.Id;
            NameKey = info.NameKey;

            GridVM = inventoryVMF.CreateItemsGrid(info.Grid);
        }

        protected override void InitSubscribes()
        {
            GridVM.AddTo(this);
        }

        public UniTask<Sprite> LoadIcon()
        {
            return inventoryVMF.RentSprite(info.IconRef);
        }
    }
}