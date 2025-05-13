using System.Collections.Generic;
using AD.Services.Router;

namespace Game.Craft
{
    public class RecyclingItemsVM : VMCollection<RecyclingItemInfo, RecyclingItemVM>
    {
        private readonly CraftVMFactory craftVMF;

        public RecyclingItemsVM(IReadOnlyCollection<RecyclingItemInfo> values, CraftVMFactory craftVMF) : base(values)
        {
            this.craftVMF = craftVMF;
        }

        protected override RecyclingItemVM Create(RecyclingItemInfo value)
        {
            return new RecyclingItemVM(value, craftVMF);
        }
    }
}