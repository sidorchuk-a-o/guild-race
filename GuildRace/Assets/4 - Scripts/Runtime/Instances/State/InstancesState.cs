using AD.Services;
using AD.Services.Save;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using VContainer;

namespace Game.Instances
{
    public class InstancesState : ServiceState<InstancesConfig, InstancesStateSM>
    {
        private readonly SeasonsCollection seasons = new(null);

        public override string SaveKey => InstancesStateSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public ISeasonsCollection Seasons => seasons;

        public InstancesState(InstancesConfig config, IObjectResolver resolver) : base(config, resolver)
        {
        }

        // == Save ==

        protected override InstancesStateSM CreateSave()
        {
            return new InstancesStateSM
            {
                Seasons = seasons
            };
        }

        protected override void ReadSave(InstancesStateSM save)
        {
            if (save == null)
            {
                seasons.AddRange(CreateDefaultSeasons());

                return;
            }

            seasons.AddRange(save.GetSeasons(config));
        }

        private IEnumerable<SeasonInfo> CreateDefaultSeasons()
        {
            return config.Seasons.Select(seasonData =>
            {
                var raidData = seasonData.Instances.FirstOrDefault(x => x.Type == InstanceTypes.raid);
                var dungeonsData = seasonData.Instances.Where(x => x.Type == InstanceTypes.dungeon);

                var raid = new InstanceInfo(raidData);
                var dungeons = dungeonsData.Select(x => new InstanceInfo(x));

                return new SeasonInfo(seasonData, raid, dungeons);
            });
        }
    }
}