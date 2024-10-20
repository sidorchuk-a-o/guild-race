using Game.Items;
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
        [ES3Serializable] private EquipSlotsSM equipSlotsSM;

        public CharacterSM(CharacterInfo info, IItemsDatabaseService itemsDatabase)
        {
            id = info.Id;
            nickname = info.Nickname;
            classId = info.ClassId;
            specId = info.SpecId.Value;
            guildRankId = info.GuildRankId.Value;
            equipSlotsSM = itemsDatabase.CreateSlotsSave(info.EquipSlots);
        }

        public CharacterInfo GetValue(IItemsDatabaseService itemsDatabase)
        {
            var equipSlots = itemsDatabase.ReadSlotsSave(equipSlotsSM);
            var info = new CharacterInfo(id, nickname, classId, equipSlots);

            info.SetGuildRank(guildRankId);
            info.SetSpecialization(specId);

            return info;
        }
    }
}