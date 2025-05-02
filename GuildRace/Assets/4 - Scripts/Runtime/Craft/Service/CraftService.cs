using AD.Services;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Guild;
using Game.Instances;
using Game.Inventory;
using System;
using System.Linq;
using UniRx;
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

        private readonly Subject<CraftingResult> onCraftingComplete = new();

        public IVendorsCollection Vendors => state.Vendors;
        public RecycleSlotInfo RecycleSlot => state.RecycleSlot;

        public IObservable<CraftingResult> OnCraftingComplete => onCraftingComplete;

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

            state = new(craftConfig, guildService, inventoryService, resolver);
        }

        public override async UniTask<bool> Init()
        {
            state.Init();

            InitRecycleProcess();

            return await Inited();
        }

        // == Recycling ==

        private void InitRecycleProcess()
        {
            state.RecycleSlot.Item
                .Where(x => x != null)
                .Subscribe(StartRecycleProcess);
        }

        private void StartRecycleProcess(ItemInfo item)
        {
            var recyclingParams = GetRecyclingResult(item.Id);

            // create reagent
            var newItemId = recyclingParams.ReagentId;
            var newItem = inventoryService.Factory.CreateItem(newItemId);

            if (newItem is not ReagentItemInfo reagentItem)
            {
                return;
            }

            // find bank tab
            var reagentsParams = craftConfig.ReagentsParams;
            var reagentCellTypes = reagentsParams.GridParams.CellTypes;

            var reagentsBank = guildService.BankTabs
                .Select(x => x.Grid)
                .FirstOrDefault(x => reagentCellTypes.Contains(x.CellType));

            // place in bank
            var reagentCount = recyclingParams.Count;

            reagentItem.Stack.SetValue(reagentCount);

            var placementArgs = new PlaceInPlacementArgs
            {
                ItemId = reagentItem.Id,
                PlacementId = reagentsBank.Id
            };

            inventoryService.TryPlaceItem(placementArgs);

            // remove item
            var removeArgs = new RemoveFromSlotArgs
            {
                ItemId = item.Id
            };

            inventoryService.TryRemoveItem(removeArgs);
        }

        public RecyclingResult GetRecyclingResult(string itemId)
        {
            const float recyclePercent = .3f;

            var item = inventoryService.GetItem(itemId);

            if (item is ReagentItemInfo)
            {
                return null;
            }

            var recipe = craftConfig.GetRecipeByItem(item.DataId)
                      ?? GetNearbyRecipe(item.DataId);

            var ingredient = recipe.Ingredients[0];

            var stack = GetStack(item);
            var rarity = GetRarity(item);
            var rariryMod = craftConfig.RecyclingParams.GetRarityMod(rarity);

            var recyclingResult = Mathf.CeilToInt(ingredient.Count * recyclePercent) * rariryMod.Value * stack;

            return new RecyclingResult
            {
                ReagentId = ingredient.ReagentId,
                Count = recyclingResult
            };
        }

        private RecipeData GetNearbyRecipe(int dataId)
        {
            var itemData = inventoryConfig.GetItem(dataId);
            var itemType = itemData.GetType();

            var vendors = craftConfig.Vendors.Where(vendor =>
            {
                var productItemId = vendor.Recipes[0].ProductItemId;
                var productData = inventoryConfig.GetItem(productItemId);

                return itemType == productData.GetType();
            });

            var recipe = vendors.SelectMany(x => x.Recipes).FirstOrDefault(recipe =>
            {
                var productData = inventoryConfig.GetItem(recipe.ProductItemId);

                // TODO: добавить в конфиг хелперы для сравнения предметов на переработку
                return productData switch
                {
                    EquipItemData => checkEquips(productData, itemData),
                    ConsumablesItemData => checkConsumables(productData, itemData),
                    _ => false
                };

                bool checkEquips(ItemData a, ItemData b)
                {
                    // предметы из данжа не создаются, ищем по общим параметрам
                    return a is EquipItemData ea
                        && b is EquipItemData eb
                        && ea.Slot == eb.Slot
                        && ea.Type == eb.Type;
                }

                bool checkConsumables(ItemData a, ItemData b)
                {
                    // все расходники создаются через крафт
                    return a.Id == b.Id;
                }
            });

            return recipe;
        }

        private int GetStack(ItemInfo item)
        {
            return item switch
            {
                IStackableItem stackableItem => stackableItem.Stack.Value,
                _ => 1
            };
        }

        private Rarity GetRarity(ItemInfo item)
        {
            return item switch
            {
                ConsumablesItemInfo consumables => consumables.Rarity,
                EquipItemInfo equip => equip.Rarity,
                _ => -1
            };
        }

        // == Crafting ==

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

            var craftingResultCount = 0;
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

                craftingResultCount += craftingCount;
            }

            if (craftingResultCount > 0)
            {
                var craftingResult = new CraftingResult(recipeData.ProductItemId, craftingResultCount);

                onCraftingComplete.OnNext(craftingResult);
            }
        }
    }
}