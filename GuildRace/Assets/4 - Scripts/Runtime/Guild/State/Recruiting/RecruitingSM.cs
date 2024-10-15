﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class RecruitingSM
    {
        [ES3Serializable] private JoinRequestsSM requestsSM;
        [ES3Serializable] private long nextRequestTime;
        [ES3Serializable] private ClassWeightsSM classWeightsSM;

        public IEnumerable<JoinRequestInfo> Requests
        {
            get => requestsSM.GetValues();
            set => requestsSM = new(value);
        }

        public DateTime NextRequestTime
        {
            get => new(nextRequestTime);
            set => nextRequestTime = value.Ticks;
        }

        public IEnumerable<ClassWeightInfo> ClassWeights
        {
            get => classWeightsSM.GetValues();
            set => classWeightsSM = new(value);
        }
    }
}