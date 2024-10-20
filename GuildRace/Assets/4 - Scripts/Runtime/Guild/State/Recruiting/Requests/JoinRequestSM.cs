using Game.Items;
using Newtonsoft.Json;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class JoinRequestSM
    {
        [ES3Serializable] private long createdTime;
        [ES3Serializable] private CharacterSM characterSM;

        public JoinRequestSM(JoinRequestInfo info, IItemsDatabaseService itemsDatabase)
        {
            createdTime = info.CreatedTime.Ticks;
            characterSM = new CharacterSM(info.Character, itemsDatabase);
        }

        public JoinRequestInfo GetValue(IItemsDatabaseService itemsDatabase)
        {
            var character = characterSM.GetValue(itemsDatabase);

            return new(character, new(createdTime));
        }
    }
}