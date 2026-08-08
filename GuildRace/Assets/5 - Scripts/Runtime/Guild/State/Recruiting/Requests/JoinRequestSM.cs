using AD.Game.Inventory;
using Newtonsoft.Json;

namespace AD.Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class JoinRequestSM
    {
        [ES3Serializable] private long createdTime;
        [ES3Serializable] private CharacterSM characterSM;

        public JoinRequestSM(JoinRequestInfo info, IInventoryService inventoryService)
        {
            createdTime = info.CreatedTime.Ticks;
            characterSM = new(info.Character, inventoryService);
        }

        public JoinRequestInfo GetValue(IInventoryService inventoryService)
        {
            var character = characterSM.GetValue(inventoryService);

            return new(character, new(createdTime));
        }
    }
}