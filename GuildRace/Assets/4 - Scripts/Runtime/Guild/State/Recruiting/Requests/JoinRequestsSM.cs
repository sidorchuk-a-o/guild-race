using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class JoinRequestsSM
    {
        [ES3Serializable] private List<JoinRequestSM> values;

        public JoinRequestsSM(IEnumerable<JoinRequestInfo> values)
        {
            this.values = values
                .Select(x => new JoinRequestSM(x))
                .ToList();
        }

        public IEnumerable<JoinRequestInfo> GetValues()
        {
            return values.Select(x => x.GetValue());
        }
    }
}