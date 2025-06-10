using System.Collections.Generic;
using AD.ToolsCollection;

namespace Game.Instances
{
    public class InstancesEditorState : EditorState<InstancesEditorState>
    {
        private InstancesConfig config;
        private InstancesEditorsFactory editorsFactory;

        private InstancesScriptGenerator scriptGenerator = new();

        public static InstancesConfig Config => FindAsset(ref instance.config);
        public static InstancesEditorsFactory EditorsFactory => instance.editorsFactory ??= new();

        // == Collections ==

        public static Collection<int> GetInstanceTypesCollection()
        {
            return Config.CreateKeyCollection<InstanceTypeData, int>("instanceTypes");
        }

        public static Collection<int> GetInstanceTypesViewCollection()
        {
            return Config.CreateKeyViewCollection<InstanceTypeData, int>("instanceTypes");
        }

        public static Collection<int> GetConsumableTypesViewCollection()
        {
            return Config.ConsumablesParams.CreateKeyViewCollection<ConsumableTypeData, int>("types");
        }

        public static Collection<int> CreateConsumableMechanicsViewCollection()
        {
            return Config.ConsumablesParams.CreateKeyViewCollection<ConsumableMechanicHandler, int>("mechanicHandlers");
        }

        public static Collection<int> CreateConsumableMechanicsCollection()
        {
            return Config.ConsumablesParams.CreateKeyCollection<ConsumableMechanicHandler, int>("mechanicHandlers");
        }

        public static Collection<int> CreateRewardMechanicsViewCollection()
        {
            return Config.RewardsParams.CreateKeyViewCollection<RewardHandler, int>("rewardHandlers");
        }

        public static Collection<int> GetThreatsViewCollection()
        {
            return Config.CreateKeyViewCollection<ThreatData, int>("threats");
        }

        public static Collection<int> GetInstancesCollection()
        {
            var seasons = Config.GetValue<List<SeasonData>>("seasons");

            var values = new List<int>();
            var options = new List<string>();

            foreach (var season in seasons)
            {
                var seasonName = season.Title;

                foreach (var instance in season.Instances)
                {
                    var option = $"{seasonName} / {instance.Title}";

                    values.Add(instance.Id);
                    options.Add(option);
                }
            }

            return new Collection<int>(values, options, autoSort: false);
        }

        public static Collection<int> GetBossesCollection()
        {
            var seasons = Config.GetValue<List<SeasonData>>("seasons");

            var values = new List<int>();
            var options = new List<string>();

            foreach (var season in seasons)
            {
                var seasonName = season.Title;

                foreach (var instance in season.Instances)
                {
                    var instanceName = instance.Title;

                    foreach (var bossUnit in instance.BoosUnits)
                    {
                        var option = $"{seasonName} / {instanceName} / {bossUnit.Title}";

                        values.Add(bossUnit.Id);
                        options.Add(option);
                    }
                }
            }

            return new Collection<int>(values, options, autoSort: false);
        }
    }
}