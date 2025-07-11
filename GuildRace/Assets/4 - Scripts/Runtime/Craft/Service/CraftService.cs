﻿using AD.Services;
using AD.Services.Store;
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
        private readonly IStoreService storeService;

        private readonly Subject<CraftingResult> onCraftingComplete = new();

        public IVendorsCollection Vendors => state.Vendors;
        public RecycleSlotInfo RecycleSlot => state.RecycleSlot;

        public IObservable<CraftingResult> OnCraftingComplete => onCraftingComplete;

        public CraftService(
            CraftConfig craftConfig,
            InventoryConfig inventoryConfig,
            IGuildService guildService,
            IInventoryService inventoryService,
            IStoreService storeService,
            IObjectResolver resolver)
        {
            this.craftConfig = craftConfig;
            this.inventoryConfig = inventoryConfig;

            this.guildService = guildService;
            this.inventoryService = inventoryService;
            this.storeService = storeService;
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

        private async void StartRecycleProcess(ItemInfo item)
        {
            var result = GetRecyclingResult(item.Id);

            if (result is RecyclingReagentResult recyclingReagent)
            {
                var amount = recyclingReagent.Amount;

                storeService.CurrenciesModule.AddCurrency(amount);
            }

            if (result is RecyclingItemResult recyclingItem)
            {
                // find bank tab
                var reagentsParams = craftConfig.ReagentsParams;
                var reagentCellTypes = reagentsParams.GridParams.CellTypes;

                var reagentsBank = guildService.BankTabs
                    .Select(x => x.Grid)
                    .FirstOrDefault(x => reagentCellTypes.Contains(x.CellType));

                foreach (var reagentData in recyclingItem.Reagents)
                {
                    // create reagent
                    var newItemId = reagentData.ReagentId;
                    var newItem = inventoryService.Factory.CreateItem(newItemId);

                    if (newItem is not ReagentItemInfo reagentItem)
                    {
                        return;
                    }

                    // place in bank
                    var reagentCount = reagentData.Count;

                    reagentItem.Stack.SetValue(reagentCount);

                    var placementArgs = new PlaceInPlacementArgs
                    {
                        ItemId = reagentItem.Id,
                        PlacementId = reagentsBank.Id
                    };

                    inventoryService.TryPlaceItem(placementArgs);
                }
            }

            await UniTask.Yield();

            inventoryService.TryDiscardItem(new DiscardItemArgs
            {
                ItemId = item.Id,
                SlotId = state.RecycleSlot.Id
            });
        }

        public RecyclingResult GetRecyclingResult(string itemId)
        {
            var item = inventoryService.GetItem(itemId);
            var recyclingParams = craftConfig.RecyclingParams;

            var stack = GetStack(item);
            var rarity = GetRarity(item);

            if (item is ReagentItemInfo)
            {
                var reagentMod = recyclingParams.GetRecyclingReagent(rarity);

                return new RecyclingReagentResult
                {
                    Amount = new(reagentMod.CurrencyKey, reagentMod.Amount * stack)
                };
            }
            else
            {
                var recipe = craftConfig.GetRecipeByItem(item.DataId)
                          ?? GetNearbyRecipe(item.DataId);

                var ignoreReagents = recyclingParams.IgnoreReagents;
                var ingredients = recipe.Ingredients.Where(x => !ignoreReagents.Contains(x.ReagentId));

                var itemMod = recyclingParams.GetRecyclingItem(rarity);

                var reagents = ingredients.Select(ingredient =>
                {
                    var reagentId = ingredient.ReagentId;
                    var recyclingResult = Mathf.CeilToInt(ingredient.Count * itemMod.Percent) * stack;

                    return new RecyclingItemInfo
                    {
                        ReagentId = reagentId,
                        Count = recyclingResult
                    };
                });

                return new RecyclingItemResult
                {
                    Reagents = reagents.ToList()
                };
            }
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