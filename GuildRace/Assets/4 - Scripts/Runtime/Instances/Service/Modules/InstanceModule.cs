using AD.Services.Router;
using Cysharp.Threading.Tasks;
using Game.Guild;
using Game.Inventory;
using System.Linq;

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

        public async UniTask StartSetupInstance(int instanceId)
        {
            state.CreateSetupInstance(instanceId);

            await router.PushAsync(
                pathKey: RouteKeys.Instances.setupInstance,
                loadingKey: LoadingScreenKeys.loading);
        }

        public void TryAddCharacterToSquad(string characterId)
        {
            var setupInstance = state.SetupInstance;

            if (setupInstance == null)
            {
                return;
            }

            var setupInstanceId = setupInstance.Id;
            var instance = setupInstance.Instance;

            var character = guildService.Characters[characterId];
            var characterRole = guildConfig.CharactersParams.GetRoleBySpec(character.SpecId.Value);

            var squad = setupInstance.Squad.Select(id => guildService.Characters[id]);
            var squadParams = instancesConfig.SquadParams.GetSquadParams(instance.Type);
            var roleParams = squadParams.GetRole(characterRole);

            var roleCountInSquad = squad
                .Select(x => guildConfig.CharactersParams.GetRoleBySpec(x.SpecId.Value))
                .Count(role => role == characterRole);

            if (roleCountInSquad >= roleParams.MaxUnitsCount)
            {
                return;
            }

            character.SetInstanceId(setupInstanceId);
            setupInstance.AddCharacter(characterId);
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

            character.SetInstanceId(null);
            setupInstance.RemoveCharacter(characterId);
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

            foreach (var characterId in instance.Squad)
            {
                var character = guildService.Characters[characterId];

                character.SetInstanceId(null);
            }

            // reset bag

            var bagCellType = instancesConfig.SquadParams.Bag.CellType;
            var guildTab = guildService.BankTabs.FirstOrDefault(x => x.Grid.CellType == bagCellType);

            foreach (var item in instance.Bag.Items)
            {
                inventoryService.TryPlaceItem(new PlaceInPlacementArgs
                {
                    ItemId = item.Id,
                    PlacementId = guildTab.Grid.Id
                });
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
    }
}