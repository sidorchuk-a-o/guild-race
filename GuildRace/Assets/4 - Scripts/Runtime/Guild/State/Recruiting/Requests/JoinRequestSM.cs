using Game.Items;
using Newtonsoft.Json;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class JoinRequestSM
    {
        [ES3Serializable] private long createdTime;
        [ES3Serializable] private CharacterSM characterSM;

        public JoinRequestSM(JoinRequestInfo info, IItemsService itemsService)
        {
            createdTime = info.CreatedTime.Ticks;
            characterSM = new CharacterSM(info.Character, itemsService);
        }

        public JoinRequestInfo GetValue(IItemsService itemsService)
        {
            var character = characterSM.GetValue(itemsService);

            return new(character, new(createdTime));
        }
    }
}