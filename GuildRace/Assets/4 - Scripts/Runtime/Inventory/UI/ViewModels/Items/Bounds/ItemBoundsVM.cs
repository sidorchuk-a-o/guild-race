using AD.Services.Router;
using AD.ToolsCollection;
using System;
using UnityEngine;

namespace Game.Inventory
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

        protected override void InitSubscribes(CompositeDisp disp)
        {
        }

        public void Rotate()
        {
            info.Rotate();
        }

        public IObservable<BoundsInt> ObserveValue()
        {
            return info;
        }
    }
}