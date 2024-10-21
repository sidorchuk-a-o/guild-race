using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Game.Items
{
    [JsonObject(MemberSerialization.Fields)]
    public class EquipSlotsSM
    {
        [ES3Serializable] private List<EquipSlotSM> values;

        public EquipSlotsSM(IEnumerable<EquipSlotInfo> values, IItemsService itemsService)
        {
            this.values = values
                .Select(x => itemsService.CreateSlotSave(x))
                .ToList();
        }

        public IEnumerable<EquipSlotInfo> GetValues(IItemsService itemsService)
        {
            return values.Select(x => itemsService.ReadSlotSave(x));
        }
    }
}