using Game.Items;
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

        public JoinRequestsSM(IEnumerable<JoinRequestInfo> values, IItemsDatabaseService itemsDatabase)
        {
            this.values = values
                .Select(x => new JoinRequestSM(x, itemsDatabase))
                .ToList();
        }

        public IEnumerable<JoinRequestInfo> GetValues(IItemsDatabaseService itemsDatabase)
        {
            return values.Select(x => x.GetValue(itemsDatabase));
        }
    }
}