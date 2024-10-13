using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class CharactersSM
    {
        [ES3Serializable] private List<CharacterSM> values;

        public CharactersSM(IEnumerable<CharacterInfo> values)
        {
            this.values = values
                .Select(x => new CharacterSM(x))
                .ToList();
        }

        public IEnumerable<CharacterInfo> GetValues()
        {
            return values.Select(x => x.GetValue());
        }
    }
}