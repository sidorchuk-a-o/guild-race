using AD.Services;
using AD.Services.Save;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Inventory;
using System.Collections.Generic;
using System.Linq;
using VContainer;

namespace Game.Instances
{
    public class InstancesState : ServiceState<InstancesConfig, InstancesStateSM>
    {
        private readonly IInventoryService inventoryService;

        private readonly SeasonsCollection seasons = new(null);
        private readonly ActiveInstancesCollection activeInstances = new(null);

        public override string SaveKey => InstancesStateSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public ISeasonsCollection Seasons => seasons;
        public IActiveInstancesCollection ActiveInstances => activeInstances;

        public bool HasPlayerInstance => PlayerInstance != null;
        public ActiveInstanceInfo SetupInstance { get; private set; }
        public ActiveInstanceInfo PlayerInstance { get; private set; }

        public InstancesState(InstancesConfig config, IInventoryService inventoryService, IObjectResolver resolver) : base(config, resolver)
        {
            this.inventoryService = inventoryService;
        }

        public void CreateSetupInstance(int instanceId)
        {
            var id = GuidUtils.Generate();
            var bagGrid = inventoryService.Factory.CreateGrid(config.SquadParams.Bag);

            SetupInstance = new(id, instanceId, bagGrid, squad: null);
        }

        public void CancelSetupInstance()
        {
            SetupInstance = null;
        }

        public void CompleteSetupAndStartInstance(bool playerInstance)
        {
            activeInstances.Add(SetupInstance);

            if (playerInstance)
            {
                PlayerInstance = SetupInstance;
            }

            SetupInstance = null;

            MarkAsDirty(true);
        }

        public int RemoveActiveInstance(string activeInstanceId)
        {
            var index = activeInstances.FindIndex(x =>
            {
                return x.Id == activeInstanceId;
            });

            var activeInstance = activeInstances[index];

            activeInstances.RemoveAt(index);

            if (PlayerInstance == activeInstance)
            {
                PlayerInstance = null;
            }

            MarkAsDirty(true);

            return index;
        }

        // == Save ==

        protected override InstancesStateSM CreateSave()
        {
            var instancesStateSM = new InstancesStateSM
            {
                PlayerInstanceId = PlayerInstance?.Id
            };

            instancesStateSM.SetSeasons(seasons);
            instancesStateSM.SetActiveInstances(activeInstances, inventoryService);

            return instancesStateSM;
        }

        protected override void ReadSave(InstancesStateSM save)
        {
            if (save == null)
            {
                seasons.AddRange(CreateDefaultSeasons());

                return;
            }

            seasons.AddRange(save.GetSeasons(config));
            activeInstances.AddRange(save.GetActiveInstances(inventoryService));

            PlayerInstance = activeInstances.GetInstance(save.PlayerInstanceId);

            CreateNewSeasons();
        }

        private IEnumerable<SeasonInfo> CreateDefaultSeasons()
        {
            return config.Seasons.Select(seasonData =>
            {
                return CreateISeason(seasonData);
            });
        }

        private void CreateNewSeasons()
        {
            foreach (var seasonData in config.Seasons)
            {
                if (seasons.Any(x => x.Id == seasonData.Id))
                {
                    continue;
                }

                var season = CreateISeason(seasonData);

                seasons.Add(season);
            }
        }

        private static SeasonInfo CreateISeason(SeasonData seasonData)
        {
            var raidData = seasonData.Instances.FirstOrDefault(x => x.Type == InstanceTypes.raid);
            var dungeonsData = seasonData.Instances.Where(x => x.Type == InstanceTypes.dungeon);

            var raid = new InstanceInfo(raidData);
            var dungeons = dungeonsData.Select(x => new InstanceInfo(x));

            return new SeasonInfo(seasonData, raid, dungeons);
        }
    }
}