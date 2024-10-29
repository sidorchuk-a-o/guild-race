using Game.Inventory;
using Newtonsoft.Json;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class CharacterSM
    {
        [ES3Serializable] private string id;
        [ES3Serializable] private string nickname;
        [ES3Serializable] private string classId;
        [ES3Serializable] private string specId;
        [ES3Serializable] private string guildRankId;
        [ES3Serializable] private ItemSlotsSM equipSlotsSM;

        public CharacterSM(CharacterInfo info, IInventoryService inventoryService)
        {
            id = info.Id;
            nickname = info.Nickname;
            classId = info.ClassId;
            specId = info.SpecId.Value;
            guildRankId = info.GuildRankId.Value;
            equipSlotsSM = inventoryService.Factory.CreateSlotsSave(info.EquipSlots);
        }

        public CharacterInfo GetValue(IInventoryService inventoryService)
        {
            var equipSlots = inventoryService.Factory.ReadSlotsSave(equipSlotsSM);
            var info = new CharacterInfo(id, nickname, classId, equipSlots);

            info.Init();
            info.SetGuildRank(guildRankId);
            info.SetSpecialization(specId);

            return info;
        }
    }
}