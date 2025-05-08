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
            // create
            state.CreateSetupInstance(args);

            // mark as dirty
            state.MarkAsDirty();
            guildService.StateMarkAsDirty();

            // setup
            await router.PushAsync(
                pathKey: RouteKeys.Instances.setupInstance,
                loadingKey: LoadingScreenKeys.loading);
        }

        public IReadOnlyCollection<SquadCandidateInfo> GetSquadCandidates()
        {
            var bossThreats = state.SetupInstance.BossUnit.GetThreats();

            var candidates = guildService.Characters.Select(character =>
            {
                var threats = GetCharacterThreats(character, bossThreats);

                return new SquadCandidateInfo(character.Id, threats);
            });

            return candidates.ToList();
        }

        private IEnumerable<ThreatInfo> GetCharacterThreats(CharacterInfo character, ThreatId[] bossThreats)
        {
            var specData = guildConfig.CharactersParams.GetSpecialization(character.SpecId);
            var specThreats = specData.GetThreats();

            var threats = specThreats.Select(t =>
            {
                var threatResolved = bossThreats.Contains(t);

                return new ThreatInfo(t, threatResolved);
            });

            return threats;
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

            var bossThreats = setupInstance.BossUnit.GetThreats();
            var resolvedThreats = GetCharacterThreats(character, bossThreats).Where(x => x.Resolved.Value);

            character.SetInstanceId(setupInstanceId);

            var squadUnit = new SquadUnitInfo(characterId, characterBag, resolvedThreats);

            setupInstance.AddUnit(squadUnit);

            UpdateThreats();

            // mark as dirty
            state.MarkAsDirty();
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

            UpdateThreats();

            // mark as dirty
            state.MarkAsDirty();
        }

        private void UpdateThreats()
        {
            var setupInstance = state.SetupInstance;

            var characters = setupInstance.Squad
                .Select(x => guildService.Characters[x.CharactedId]);

            var characterSpecs = characters
                .Select(x => x.SpecId).Distinct()
                .Select(x => guildConfig.CharactersParams.GetSpecialization(x));

            var resolvedThreats = characterSpecs
                .SelectMany(x => x.GetThreats())
                .Distinct();

            setupInstance.ApplyResolveThreats(resolvedThreats);
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

            // mark as dirty
            guildService.StateMarkAsDirty();
            state.MarkAsDirty();

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

            // mark as dirty
            guildService.StateMarkAsDirty();
            state.MarkAsDirty();
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

            // reset boss

            instance.BossUnit.SetInstanceId(null);
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