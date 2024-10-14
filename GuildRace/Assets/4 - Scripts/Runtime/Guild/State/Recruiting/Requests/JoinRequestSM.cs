using Newtonsoft.Json;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class JoinRequestSM
    {
        [ES3Serializable] private long createdTime;
        [ES3Serializable] private CharacterSM characterSM;

        public JoinRequestSM(JoinRequestInfo info)
        {
            createdTime = info.CreatedTime;
            characterSM = new CharacterSM(info.Character);
        }

        public JoinRequestInfo GetValue()
        {
            var character = characterSM.GetValue();

            return new(character, createdTime);
        }
    }
}