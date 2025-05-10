using Game.Inventory;
using Newtonsoft.Json;

namespace Game.Instances
{
    [JsonObject(MemberSerialization.Fields)]
    public class UnitBagSM : ItemsGridSM
    {
        public UnitBagSM(UnitBagInfo info, IInventoryFactory inventoryFactory)
            : base(info, inventoryFactory)
        {
        }

        public UnitBagInfo GetValue(UnitBagData data, IInventoryFactory inventoryFactory)
        {
            var items = itemsSM.GetCollection(inventoryFactory);

            return new UnitBagInfo(id, data, items);
        }
    }
}