using AD.Services.Localization;
using AD.Services.Router;

namespace Game.Inventory
{
    public class EquipTypeVM : ViewModel
    {
        public EquipType Key { get; }
        public LocalizeKey NameKey { get; }

        public EquipTypeVM(EquipTypeData data)
        {
            Key = data.Id;
            NameKey = data.NameKey;
        }

        protected override void InitSubscribes()
        {
        }
    }
}