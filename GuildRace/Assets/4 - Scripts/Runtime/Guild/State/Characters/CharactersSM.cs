using Game.Items;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class CharactersSM
    {
        [ES3Serializable] private List<CharacterSM> values;

        public CharactersSM(IEnumerable<CharacterInfo> values, IItemsDatabaseService itemsService)
        {
            this.values = values
                .Select(x => new CharacterSM(x, itemsService))
                .ToList();
        }

        public IEnumerable<CharacterInfo> GetValues(IItemsDatabaseService itemsService)
        {
            return values.Select(x => x.GetValue(itemsService));
        }
    }
}