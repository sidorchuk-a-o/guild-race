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

        public CharacterSM(CharacterInfo info)
        {
            id = info.Id;
            nickname = info.Nickname;
            classId = info.ClassId;
            specId = info.SpecId.Value;
            guildRankId = info.GuildRankId.Value;
        }

        public CharacterInfo GetValue()
        {
            var info = new CharacterInfo(id, nickname, classId);

            info.SetGuildRank(guildRankId);
            info.SetSpecialization(specId);

            return info;
        }
    }
}