using AD.Game.Inventory;
using Newtonsoft.Json;

namespace AD.Game.Instances
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

            return new(id, data, items);
        }
    }
}