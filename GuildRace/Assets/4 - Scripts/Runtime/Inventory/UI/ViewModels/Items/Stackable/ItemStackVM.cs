using AD.Services.Router;
using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    public class ItemStackVM : ViewModel
    {
        private readonly ItemStackInfo info;

        public int Size => info.Size;
        public int Value => info.Value;

        public bool IsFulled => info.IsFulled;
        public int AvailableSpace => info.AvailableSpace;

        public ItemStackVM(ItemStackInfo info)
        {
            this.info = info;
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
        }

        public IObservable<int> ObserveValue()
        {
            return info.ObserveValue();
        }
    }
}