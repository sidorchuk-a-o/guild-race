using System.Collections.Generic;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    public class JoinRequestsCollection : ReactiveCollection<JoinRequestInfo>, IJoinRequestsCollection
    {
        public JoinRequestsCollection(IEnumerable<JoinRequestInfo> values) : base(values)
        {
        }
    }
}