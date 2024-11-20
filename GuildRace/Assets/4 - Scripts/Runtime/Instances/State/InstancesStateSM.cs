﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Game.Instances
{
    [JsonObject(MemberSerialization.Fields)]
    public class InstancesStateSM
    {
        public const string key = "instances";

        [ES3Serializable] private SeasonsSM seasonsSM;

        public IEnumerable<SeasonInfo> Seasons
        {
            set => seasonsSM = new(value);
        }

        public IEnumerable<SeasonInfo> GetSeasons(InstancesConfig config)
        {
            return seasonsSM.GetValues(config);
        }
    }
}