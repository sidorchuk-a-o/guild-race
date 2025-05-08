using System;
using System.Collections.Generic;
using System.Linq;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using Game.Guild;
using Game.Inventory;

namespace Game.Instances
{
    public class InstanceModule
    {
        private readonly InstancesState state;
        private readonly GuildConfig guildConfig;
        private readonly InstancesConfig instancesConfig;

        private readonly IRouterService router;
        private readonly IGuildService guildService;
        private readonly IInventoryService inventoryService;

        public InstanceModule(
            InstancesState state,
            GuildConfig guildConfig,
            InstancesConfig instancesConfig,
            IRouterService router,
            IGuildService guildService,
            IInventoryService inventoryService)
        {
            this.state = state;
            this.guildConfig = guildConfig;
            this.instancesConfig = instancesConfig;
            this.router = router;
            this.guildService = guildService;
            this.inventoryService = inventoryService;
        }

        public async UniTask StartSetupInstance(SetupInstanceArgs args)
        {
            state.CreateSetupInstance(args);

            await router.PushAsync(
                pathKey: RouteKeys.Instances.setupInstance,
                loadingKey: LoadingScreenKeys.loading);
        }

        public IReadOnlyCollection<SquadCandidateInfo> GetSquadCandidates()
        {
            var bossThreats = state.SetupInstance.BossUnit.GetThreats();

            var candidates = guildService.Characters.Select(x =>
            {
                var specData = guildConfig.CharactersParams.GetSpecialization(x.SpecId);
                var specThreats = specData.GetThreats();

                var threats = specThreats.Select(t =>
                {
                    var threatResolved = bossThreats.Contains(t);

                    return new ThreatInfo(t, threatResolved);
                });

                return new SquadCandidateInfo(x.Id, threats);
            });

            return candidates.ToList();
        }

        public void TryAddCharacterToSquad(string characterId)
        {
            var setupInstance = state.SetupInstance;

            if (setupInstance == null)
            {
                return;
            }

            var instance = setupInstance.Instance;
            var squadParams = instancesConfig.SquadParams.GetSquadParams(instance.Type);

            if (setupInstance.Squad.Count >= squadParams.MaxUnitsCount)
            {
                return;
            }

            var setupInstanceId = setupInstance.Id;
            var character = guildService.Characters[characterId];
            var characterBag = inventoryService.Factory.CreateGrid(instancesConfig.SquadParams.Bag);

            character.SetInstanceId(setupInstanceId);

            var squadUnit = new SquadUnitInfo(characterId, characterBag);

            setupInstance.AddUnit(squadUnit);
        }

        public void TryRemoveCharacterFromSquad(string characterId)
        {
            var setupInstance = state.SetupInstance;

            if (setupInstance == null)
            {
                return;
            }

            var setupInstanceId = setupInstance.Id;
            var character = guildService.Characters[characterId];

            if (character.HasInstance == false ||
                character.InstanceId.Value != setupInstanceId)
            {
                return;
            }

            var squadUnit = setupInstance.Squad.FirstOrDefault(x => x.CharactedId == characterId);

            character.SetInstanceId(null);
            setupInstance.RemoveUnit(squadUnit);

            ResetUnitBag(squadUnit.Bag);
        }

        public async UniTask CompleteSetupAndStartInstance()
        {
            var setupInstance = state.SetupInstance;

            if (setupInstance == null)
            {
                return;
            }

            // upd state
            state.CompleteSetupAndStartInstance();

            // start
            await router.PushAsync(
                pathKey: RouteKeys.Hub.activeInstances,
                parameters: RouteParams.FirstRoute);
        }

        public void CancelSetupInstance()
        {
            var setupInstance = state.SetupInstance;

            if (setupInstance == null)
            {
                return;
            }

            // reset instance

            ResetInstanceData(setupInstance);

            // cancel

            state.CancelSetupInstance();
        }

        private void ResetInstanceData(ActiveInstanceInfo instance)
        {
            // reset characters

            foreach (var squadUnit in instance.Squad)
            {
                var character = guildService.Characters[squadUnit.CharactedId];

                character.SetInstanceId(null);

                ResetUnitBag(squadUnit.Bag);
            }
        }

        public int StopActiveInstance(string activeInstanceId)
        {
            // reset instance

            var activeInstance = state.ActiveInstances.GetInstance(activeInstanceId);

            ResetInstanceData(activeInstance);

            // upd state

            return state.RemoveActiveInstance(activeInstanceId);
        }

        private void ResetUnitBag(ItemsGridInfo bag)
        {
            var bagCellType = instancesConfig.SquadParams.Bag.CellType;
            var guildTab = guildService.BankTabs.FirstOrDefault(x => x.Grid.CellType == bagCellType);

            foreach (var item in bag.Items)
            {
                inventoryService.TryPlaceItem(new PlaceInPlacementArgs
                {
                    ItemId = item.Id,
                    PlacementId = guildTab.Grid.Id
                });
            }
        }
    }
}