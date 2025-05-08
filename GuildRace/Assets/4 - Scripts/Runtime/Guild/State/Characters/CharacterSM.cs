using Game.Inventory;
using Newtonsoft.Json;
using System.Linq;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class CharacterSM
    {
        [ES3Serializable] private string id;
        [ES3Serializable] private string nickname;
        [ES3Serializable] private int classId;
        [ES3Serializable] private int specId;
        [ES3Serializable] private string instanceId;
        [ES3Serializable] private int guildRankId;
        [ES3Serializable] private ItemSlotsSM equipSlotsSM;

        public CharacterSM(CharacterInfo info, IInventoryService inventoryService)
        {
            id = info.Id;
            nickname = info.Nickname;
            classId = info.ClassId;
            specId = info.SpecId;
            instanceId = info.InstanceId.Value;
            guildRankId = info.GuildRankId.Value;
            equipSlotsSM = new(info.EquipSlots, inventoryService.Factory);
        }

        public CharacterInfo GetValue(IInventoryService inventoryService)
        {
            var equipSlots = equipSlotsSM
                .GetValues(inventoryService.Factory)
                .Cast<EquipSlotInfo>();

            var info = new CharacterInfo(id, nickname, classId, specId, equipSlots);

            info.Init();
            info.SetGuildRank(guildRankId);
            info.SetInstanceId(instanceId);

            return info;
        }
    }
}