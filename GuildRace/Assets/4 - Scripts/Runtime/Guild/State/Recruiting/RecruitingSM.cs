using Game.Items;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class RecruitingSM
    {
        public const string key = "recruiting";

        [ES3Serializable] private bool isEnabled;
        [ES3Serializable] private JoinRequestsSM requestsSM;
        [ES3Serializable] private long nextRequestTime;
        [ES3Serializable] private ClassRoleSelectorsSM classRoleSelectorSM;

        public bool IsEnabled
        {
            get => isEnabled;
            set => isEnabled = value;
        }

        public DateTime NextRequestTime
        {
            get => new(nextRequestTime);
            set => nextRequestTime = value.Ticks;
        }

        public IEnumerable<ClassRoleSelectorInfo> ClassRoleSelectors
        {
            get => classRoleSelectorSM.GetValues();
            set => classRoleSelectorSM = new(value);
        }

        public void SetRequests(IEnumerable<JoinRequestInfo> value, IItemsDatabaseService itemsService)
        {
            requestsSM = new(value, itemsService);
        }

        public IEnumerable<JoinRequestInfo> GetRequests(IItemsDatabaseService itemsService)
        {
            return requestsSM.GetValues(itemsService);
        }
    }
}