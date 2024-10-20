using AD.Services.Pools;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Items
{
    public class ItemsVMFactory : VMFactory
    {
        private readonly PoolContainer<Sprite> spritesPool;

        public ItemsVMFactory(IPoolsService pools)
        {
            spritesPool = pools.CreateAssetPool<Sprite>();
        }

        public UniTask<Sprite> LoadSprite(AssetReference spriteRef)
        {
            return spritesPool.RentAsync(spriteRef);
        }

        public EquipSlotsVM CreateEquipSlots(IEquipSlotsCollection equipSlots)
        {
            return new EquipSlotsVM(equipSlots, this);
        }

        public EquipSlotVM CreateEquipSlot(EquipSlotInfo info)
        {
            return new EquipSlotVM(info, this);
        }

        public EquipItemVM CreateEquipItem(EquipItemInfo info)
        {
            return new EquipItemVM(info, this);
        }
    }
}