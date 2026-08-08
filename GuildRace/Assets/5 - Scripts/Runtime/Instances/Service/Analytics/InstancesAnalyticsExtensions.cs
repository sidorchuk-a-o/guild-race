using AD.Analytics;
using AD.Game.Guild;
using AD.Game.Inventory;
using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Instances
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
            var instanceParams = AnalyticsParams.Empty;

            instanceParams.AddInstance(instance);
            parameters[instance.Result.CurrentValue] = instanceParams;

            analytics?.SendEvent("complete_instance", parameters);
        }

        public static void AddInstance(this AnalyticsParams parameters, ActiveInstanceInfo instance)
        {
            var instanceParams = AnalyticsParams.Empty;
            var bossParams = AnalyticsParams.Empty;
            var classesParams = AnalyticsParams.Empty;
            var consumablesParams = AnalyticsParams.Empty;

            for (var i = 0; i < instance.Squad.Count; i++)
            {
                var unit = instance.Squad[i];

                classesParams.AddCharacter(unit.CharactedId);
                consumablesParams.AddItems(unit.Bag.Items);
            }

            bossParams["squad_classes"] = classesParams;
            bossParams["squad_consums"] = consumablesParams.IsNullOrEmpty() ? string.Empty : consumablesParams;
            bossParams["chance"] = Mathf.RoundToInt(instance.CompleteChance.CurrentValue * 100f);

            instanceParams[instance.BossUnit.Title] = bossParams;
            parameters[instance.Instance.Title] = instanceParams;
        }
    }
}