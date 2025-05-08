using System.Collections.Generic;

namespace Game.Instances
{
    public class SquadCandidateInfo
    {
        public string CharacterId { get; }
        public IThreatCollcetion Threads { get; }

        public SquadCandidateInfo(string characterId, IEnumerable<ThreatInfo> threats)
        {
            CharacterId = characterId;
            Threads = new ThreatCollection(threats);
        }
    }
}