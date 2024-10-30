using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public class ItemSlotsSM
    {
        [ES3Serializable] private List<ItemSlotSM> values;

        public ItemSlotsSM(IEnumerable<ItemSlotInfo> values, IInventoryFactory inventoryFactory)
        {
            this.values = values
                .Select(x => inventoryFactory.CreateSlotSave(x))
                .ToList();
        }

        public IEnumerable<ItemSlotInfo> GetValues(IInventoryFactory inventoryFactory)
        {
            return values.Select(x => inventoryFactory.ReadSlotSave(x));
        }
    }
}