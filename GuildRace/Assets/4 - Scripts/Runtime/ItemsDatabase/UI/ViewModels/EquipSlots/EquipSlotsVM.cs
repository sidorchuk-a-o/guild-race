using AD.Services.Router;

namespace Game.Items
{
    public class EquipSlotsVM : VMCollection<EquipSlotInfo, EquipSlotVM>
    {
        private readonly ItemsVMFactory itemsVMF;

        public EquipSlotsVM(IEquipSlotsCollection values, ItemsVMFactory itemsVMF) : base(values)
        {
            this.itemsVMF = itemsVMF;
        }

        protected override EquipSlotVM Create(EquipSlotInfo value)
        {
            return itemsVMF.CreateEquipSlot(value);
        }
    }
}