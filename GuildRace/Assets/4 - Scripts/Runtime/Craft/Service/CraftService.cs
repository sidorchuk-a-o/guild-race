using AD.Services;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Guild;
using Game.Inventory;
using System.Linq;
using VContainer;

namespace Game.Craft
{
    public class CraftService : Service, ICraftService
    {
        private readonly CraftState state;
        private readonly CraftConfig config;

        private readonly IGuildService guildService;
        private readonly IInventoryService inventoryService;

        public IVendorsCollection Vendors => state.Vendors;

        public CraftService(
            CraftConfig config,
            IGuildService guildService,
            IInventoryService inventoryService,
            IObjectResolver resolver)
        {
            this.config = config;
            this.guildService = guildService;
            this.inventoryService = inventoryService;

            state = new(config, resolver);
        }

        public override async UniTask<bool> Init()
        {
            state.Init();

            CreateReagents();

            return await Inited();
        }

        private void CreateReagents()
        {
            var reagentsParams = config.ReagentsParams;
            var reagentCellTypes = reagentsParams.GridParams.CellTypes;

            var bankGrid = guildService.BankTabs
                .FirstOrDefault(x => reagentCellTypes.Contains(x.Grid.CellType));

            var reagents = reagentsParams.Items
                .Select(x => inventoryService.Factory.CreateItem(x))
                .OfType<ReagentItemInfo>();

            foreach (var reagent in reagents)
            {
                reagent.Stack.SetValue(100);

                var placementArgs = new PlaceInPlacementArgs
                {
                    ItemId = reagent.Id,
                    PlacementId = bankGrid.Id
                };

                inventoryService.TryPlaceItem(placementArgs);
            }
        }

        public void StartCraftingProcess(StartCraftingEM craftingEM)
        {
            // bank grid
            var reagentsParams = config.ReagentsParams;
            var reagentCellTypes = reagentsParams.GridParams.CellTypes;

            var bankGrids = guildService.BankTabs.Select(x => x.Grid);

            var resourceGrid = bankGrids
                .FirstOrDefault(x => reagentCellTypes.Contains(x.CellType));

            var recipeData = config.GetRecipe(craftingEM.RecipeId);

            // create product item
            var productItem = inventoryService.Factory.CreateItem(recipeData.ProductItemId);
            var productPlacementArgs = new PlaceInPlacementArgs
            {
                ItemId = productItem.Id
            };

            // find product placement
            var hasProductPlacement = bankGrids.Any(bankTab =>
            {
                productPlacementArgs.PlacementId = bankTab.Id;

                return inventoryService.CheckPossibilityOfPlacement(productPlacementArgs);
            });

            if (hasProductPlacement == false)
            {
                return;
            }

            // create take args
            var takeIngredientsArgs = recipeData.Ingredients
                .Select(x => createTakeArgs(x))
                .ToListPool();

            TakeItemsArgs createTakeArgs(IngredientData ingredient)
            {
                return new TakeItemsArgs
                {
                    ItemDataId = ingredient.ReagentId,
                    Count = ingredient.Count * craftingEM.Count,
                    GridId = resourceGrid.Id
                };
            }

            // try take ingredient
            var allowedToTake = takeIngredientsArgs.All(inventoryService.CheckPossibilityOfTake);

            if (allowedToTake)
            {
                foreach (var ingredient in takeIngredientsArgs)
                {
                    inventoryService.TryTakeItems(ingredient, out _);
                }
            }

            takeIngredientsArgs.ReleaseListPool();

            if (allowedToTake == false)
            {
                return;
            }

            // place product

            inventoryService.TryPlaceItem(productPlacementArgs);
        }
    }
}