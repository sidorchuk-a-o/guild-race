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

        public EquipSlotsSM(IEnumerable<EquipSlotInfo> values, IItemsDatabaseService database)
        {
            this.values = values
                .Select(x => database.CreateSlotSave(x))
                .ToList();
        }

        public IEnumerable<EquipSlotInfo> GetValues(IItemsDatabaseService database)
        {
            return values.Select(x => database.ReadSlotSave(x));
        }
    }
}