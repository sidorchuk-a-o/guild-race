using AD.Services;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Guild;
using Game.Inventory;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Game.Craft
{
    public class CraftService : Service, ICraftService
    {
        private readonly CraftState state;

        private readonly CraftConfig craftConfig;
        private readonly InventoryConfig inventoryConfig;

        private readonly IGuildService guildService;
        private readonly IInventoryService inventoryService;

        public IVendorsCollection Vendors => state.Vendors;

        public CraftService(
            CraftConfig craftConfig,
            InventoryConfig inventoryConfig,
            IGuildService guildService,
            IInventoryService inventoryService,
            IObjectResolver resolver)
        {
            this.craftConfig = craftConfig;
            this.inventoryConfig = inventoryConfig;

            this.guildService = guildService;
            this.inventoryService = inventoryService;

            state = new(craftConfig, resolver);
        }

        public override async UniTask<bool> Init()
        {
            state.Init();

            CreateReagents(); // TODO: TEMP

            return await Inited();
        }

        /// <summary>
        /// TEMP
        /// </summary>
        private void CreateReagents()
        {
            var reagentsParams = craftConfig.ReagentsParams;
            var reagentCellTypes = reagentsParams.GridParams.CellTypes;

            var reagentsBank = guildService.BankTabs
                .Select(x => x.Grid)
                .FirstOrDefault(x => reagentCellTypes.Contains(x.CellType));

            var reagents = reagentsParams.Items
                .Select(x => inventoryService.Factory.CreateItem(x))
                .OfType<ReagentItemInfo>();

            foreach (var reagent in reagents)
            {
                reagent.Stack.SetValue(50);

                var placementArgs = new PlaceInPlacementArgs
                {
                    ItemId = reagent.Id,
                    PlacementId = reagentsBank.Id
                };

                inventoryService.TryPlaceItem(placementArgs);
            }
        }

        public void StartCraftingProcess(StartCraftingEM craftingEM)
        {
            // bank grid
            var reagentsParams = craftConfig.ReagentsParams;
            var reagentCellTypes = reagentsParams.GridParams.CellTypes;

            var bankGrids = guildService.BankTabs.Select(x => x.Grid);

            var resourceGrid = bankGrids
                .FirstOrDefault(x => reagentCellTypes.Contains(x.CellType));

            var recipeData = craftConfig.GetRecipe(craftingEM.RecipeId);
            var productData = inventoryConfig.GetItem(recipeData.ProductItemId);

            // create product items

            var totalCraftingCount = craftingEM.Count;
            var productCount = productData is IStackable stackable
                ? Mathf.CeilToInt(totalCraftingCount / (float)stackable.Stack.Size)
                : totalCraftingCount;

            for (var i = 0; i < productCount; i++)
            {
                var productItem = inventoryService.Factory.CreateItem(recipeData.ProductItemId);

                // calc crafting count
                var craftingCount = 1;

                if (productItem is IStackableItem stackableItem)
                {
                    craftingCount = Mathf.Min(totalCraftingCount, stackableItem.Stack.Size);

                    stackableItem.Stack.SetValue(craftingCount);
                }

                totalCraftingCount -= craftingCount;

                // find product placement
                var productPlacementArgs = new PlaceInPlacementArgs
                {
                    ItemId = productItem.Id
                };

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
                        Count = ingredient.Count * craftingCount,
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
}