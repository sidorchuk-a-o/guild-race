using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Items
{
    public abstract class ItemVM : VMBase
    {
        private readonly ItemInfo info;
        protected readonly ItemsVMFactory itemsVMF;

        public string Id { get; }
        public string DataId { get; }

        public ItemVM(ItemInfo info, ItemsVMFactory itemsVMF)
        {
            this.info = info;
            this.itemsVMF = itemsVMF;

            Id = info.Id;
            DataId = info.DataId;
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
        }

        public async UniTask<Sprite> LoadIcon()
        {
            return await itemsVMF.LoadSprite(info.IconRef);
        }
    }
}