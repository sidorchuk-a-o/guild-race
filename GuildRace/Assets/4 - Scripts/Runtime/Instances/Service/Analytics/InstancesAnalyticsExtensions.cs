using AD.Services.Analytics;
using AD.ToolsCollection;
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
            var squadParams = AnalyticsParams.Empty;

            // squad
            for (var i = 0; i < instance.Squad.Count; i++)
            {
                var unit = instance.Squad[i];
                var unitParams = AnalyticsParams.Empty;
                var consumsParams = AnalyticsParams.Empty;

                unitParams.AddCharacter(unit.CharactedId);
                consumsParams.AddItems(unit.Bag.Items);

                squadParams[$"unit_{i}"] = unitParams;
                unitParams["consums"] = unit.Bag.Items.IsNullOrEmpty() ? null : consumsParams;
            }

            parameters["boss"] = instance.BossUnit.Title;
            parameters["squad"] = squadParams;
            parameters["chance"] = Mathf.RoundToInt(instance.CompleteChance.Value * 100f);
        }
    }
}