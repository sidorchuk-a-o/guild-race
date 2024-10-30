using Game.Inventory;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class CharactersSM
    {
        [ES3Serializable] private List<CharacterSM> values;

        public CharactersSM(IEnumerable<CharacterInfo> values, IInventoryService inventoryService)
        {
            this.values = values
                .Select(x => new CharacterSM(x, inventoryService))
                .ToList();
        }

        public IEnumerable<CharacterInfo> GetValues(IInventoryService inventoryService)
        {
            return values.Select(x => x.GetValue(inventoryService));
        }
    }
}