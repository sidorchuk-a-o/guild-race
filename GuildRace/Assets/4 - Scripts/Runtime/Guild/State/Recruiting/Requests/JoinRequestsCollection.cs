using AD.States;
using System.Collections.Generic;

namespace Game.Guild
{
    public class JoinRequestsCollection : ReactiveCollectionInfo<JoinRequestInfo>, IJoinRequestsCollection
    {
        public JoinRequestsCollection(IEnumerable<JoinRequestInfo> values) : base(values)
        {
        }
    }
}