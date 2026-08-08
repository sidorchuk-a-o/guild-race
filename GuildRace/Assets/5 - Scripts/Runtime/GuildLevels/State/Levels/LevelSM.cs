using Newtonsoft.Json;

namespace AD.Game.GuildLevels
{
    [JsonObject(MemberSerialization.Fields)]
    public class LevelSM
    {
        [ES3Serializable] private string id;
        [ES3Serializable] private bool isUnlocked;
        [ES3Serializable] private bool readyToUnlock;

        public string Id => id;

        public LevelSM(LevelInfo info)
        {
            id = info.Id;
            isUnlocked = info.IsUnlocked.CurrentValue;
            readyToUnlock = info.ReadyToUnlock.CurrentValue;
        }

        public LevelInfo GetValue(int level, LevelData data)
        {
            var info = new LevelInfo(level, data);

            if (isUnlocked) info.MarkAsUnlocked();
            if (readyToUnlock) info.MarkAsReadyUnlock();

            return info;
        }
    }
}