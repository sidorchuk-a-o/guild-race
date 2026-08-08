using System.Collections.Generic;
using System.Threading;
using AD.Game.UI;
using AD.Router;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.Pool;

namespace AD.Game.Inventory
{
    public abstract class ItemVM : ViewModel
    {
        private readonly ItemInfo info;
        protected readonly InventoryVMFactory inventoryVMF;

        public string Id { get; }

        public int DataId { get; }
        public ItemDataVM DataVM { get; }

        public ItemBoundsVM BoundsVM { get; }
        public UIStateVM HighlightStateVM { get; }

        public ReadOnlyReactiveProperty<bool> IsRemoved { get; }

        protected ItemVM(ItemInfo info, InventoryVMFactory inventoryVMF)
        {
            this.info = info;
            this.inventoryVMF = inventoryVMF;

            Id = info.Id;
            DataId = info.DataId;

            DataVM = inventoryVMF.CreateItemData(DataId);
            BoundsVM = new(info.Bounds);
            HighlightStateVM = new();

            IsRemoved = info.IsRemoved;
        }

        protected override void InitSubscribes()
        {
            DataVM.AddTo(this);
            BoundsVM.AddTo(this);
            HighlightStateVM.AddTo(this);
        }

        // == Options ==

        public virtual OptionKey GetFastOptionKey()
        {
            return OptionKeys.Common.Inspect;
        }

        public virtual List<OptionKey> GetOptionKeysPool()
        {
            var options = ListPool<OptionKey>.Get();

            options.Add(OptionKeys.Common.Inspect);
            options.Add(OptionKeys.Common.Discard);

            return options;
        }

        // == Icon ==

        public async UniTask<Sprite> LoadIcon(CancellationToken token = default)
        {
            return await inventoryVMF.RentImage(info.IconRef, token);
        }
    }
}