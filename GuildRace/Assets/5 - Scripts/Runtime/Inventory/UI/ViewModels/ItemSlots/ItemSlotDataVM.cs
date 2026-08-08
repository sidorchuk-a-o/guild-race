using AD.Localization;
using AD.Router;

namespace AD.Game.Inventory
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