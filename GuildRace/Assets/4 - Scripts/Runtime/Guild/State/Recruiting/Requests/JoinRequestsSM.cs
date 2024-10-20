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

        public JoinRequestsSM(IEnumerable<JoinRequestInfo> values, IItemsDatabaseService itemsService)
        {
            this.values = values
                .Select(x => new JoinRequestSM(x, itemsService))
                .ToList();
        }

        public IEnumerable<JoinRequestInfo> GetValues(IItemsDatabaseService itemsService)
        {
            return values.Select(x => x.GetValue(itemsService));
        }
    }
}