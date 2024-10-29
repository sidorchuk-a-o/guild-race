using AD.Services.Localization;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Inventory
{
    public abstract class ItemVM : ViewModel
    {
        private readonly ItemInfo info;
        protected readonly InventoryVMFactory inventoryVMF;

        private Sprite sprite;

        public string Id { get; }
        public string DataId { get; }
        public LocalizeKey NameKey { get; }
        public Type InfoType { get; }

        public ItemBoundsVM BoundsVM { get; }
        public UIStateVM HighlightStateVM { get; }

        public IReadOnlyReactiveProperty<bool> IsRemoved { get; }

        public ItemVM(ItemInfo info, InventoryVMFactory inventoryVMF)
        {
            this.info = info;
            this.inventoryVMF = inventoryVMF;

            Id = info.Id;
            DataId = info.DataId;
            NameKey = info.NameKey;
            InfoType = info.GetType();

            BoundsVM = new(info.Bounds);
            HighlightStateVM = new();

            IsRemoved = info.IsRemoved;
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
            BoundsVM.AddTo(disp);
            HighlightStateVM.AddTo(disp);
        }


        // == Options ==

        public virtual OptionKey GetFastOptionKey()
        {
            return OptionKeys.Common.inspect;
        }

        public virtual List<OptionKey> GetOptionKeysPool()
        {
            var options = ListPool<OptionKey>.Get();

            options.Add(OptionKeys.Common.inspect);
            options.Add(OptionKeys.Common.discard);

            return options;
        }

        // == Icon ==

        public async UniTask<Sprite> LoadIcon()
        {
            if (sprite == null)
            {
                sprite = await inventoryVMF.RentSprite(info.IconRef);
            }

            return sprite;
        }
    }
}