using AD.Services.Localization;
using AD.Services.Router;

namespace Game.Inventory
{
    public class ItemSlotDataVM : ViewModel
    {
        public LocalizeKey NameKey { get; }

        public ItemSlotDataVM(ItemSlotData data)
        {
            NameKey = data.NameKey;
        }

        protected override void InitSubscribes()
        {
        }
    }
}