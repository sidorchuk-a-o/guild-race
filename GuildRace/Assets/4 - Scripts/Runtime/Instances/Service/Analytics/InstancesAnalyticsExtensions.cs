using AD.Services.Analytics;
using Game.Guild;
using Game.Inventory;
using UnityEngine;

namespace Game.Instances
{
    public static class InstancesAnalyticsExtensions
    {
        public static void StartInstance(this IAnalyticsService analytics, ActiveInstanceInfo instance)
        {
            var parameters = AnalyticsParams.Default;
            parameters.AddInstance(instance);

            analytics?.SendEvent("start_instance", parameters);
        }

        public static void CompleteInstance(this IAnalyticsService analytics, ActiveInstanceInfo instance)
        {
            var parameters = AnalyticsParams.Default;
            parameters.AddInstance(instance);
            parameters.AddKey(instance.Result.Value);

            analytics?.SendEvent("complete_instance", parameters);
        }

        public static void AddInstance(this AnalyticsParams parameters, ActiveInstanceInfo instance)
        {
            var bossParams = AnalyticsParams.Empty;
            var squadParams = AnalyticsParams.Empty;

            // boss
            bossParams.AddKey(instance.BossUnit.Title);

            // squad
            for (var i = 0; i < instance.Squad.Count; i++)
            {
                var unit = instance.Squad[i];
                var unitParams = AnalyticsParams.Empty;
                var consumsParams = AnalyticsParams.Empty;

                unitParams.AddCharacter(unit.CharactedId);
                unitParams["consums"] = consumsParams;

                consumsParams.AddItems(unit.Bag.Items);

                squadParams[$"unit_{i}"] = unitParams;
            }

            parameters["boss"] = bossParams;
            parameters["squad"] = squadParams;
            parameters["chance"] = Mathf.RoundToInt(instance.CompleteChance.Value * 100f);
        }
    }
}