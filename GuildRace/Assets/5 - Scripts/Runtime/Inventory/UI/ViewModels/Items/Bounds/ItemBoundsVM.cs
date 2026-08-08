using AD.Router;
using R3;
using UnityEngine;

namespace AD.Game.Inventory
{
    public class ItemBoundsVM : ViewModel
    {
        private readonly ItemBoundsInfo info;

        public BoundsInt Value => info.Value;
        public Vector3Int Position => info.Position;
        public Vector3Int Size => info.Size;

        public bool IsRotated => info.IsRotated;

        public ItemBoundsVM(ItemBoundsInfo info)
        {
            this.info = info;
        }

        protected override void InitSubscribes()
        {
        }

        public void Rotate()
        {
            info.Rotate();
        }

        public Observable<BoundsInt> ObserveValue()
        {
            return info;
        }
    }
}