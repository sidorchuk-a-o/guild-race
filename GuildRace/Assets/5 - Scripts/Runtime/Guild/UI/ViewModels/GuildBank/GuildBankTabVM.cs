using AD.Game.Inventory;
using AD.Localization;
using AD.Router;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AD.Game.Guild
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
            return inventoryVMF.RentImage(info.IconRef);
        }
    }
}