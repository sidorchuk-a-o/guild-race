using Newtonsoft.Json;

namespace Game.Instances
{
    [JsonObject(MemberSerialization.Fields)]
    public class SeasonSM
    {
        [ES3Serializable] private int id;
        [ES3Serializable] private InstanceSM raidSM;
        [ES3Serializable] private InstancesSM dungeonsSM;

        public SeasonSM(SeasonInfo info)
        {
            id = info.Id;
            raidSM = new(info.Raid);
            dungeonsSM = new(info.Dungeons);
        }

        public SeasonInfo GetValue(InstancesConfig config)
        {
            var data = config.GetSeason(id);
            var raid = raidSM.GetValue(config);
            var dungeons = dungeonsSM.GetValues(config);

            return new(data, raid, dungeons);
        }
    }
}