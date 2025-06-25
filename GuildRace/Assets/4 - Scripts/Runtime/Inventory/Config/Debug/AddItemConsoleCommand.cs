using AD.Services.Debug;
using AD.Services.Localization;
using AD.ToolsCollection;
using Game.Inventory;
using System;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Game.Guild
{
    public class AddItemConsoleCommand : ConsoleCommandHandler
    {
        public delegate void AddEquip(int dataId, int count);

        private InventoryConfig inventoryConfig;
        private IInventoryService inventoryService;
        private ILocalizationService localization;

        public override string Name => "add_item";
        public override string Desc => "Добавить предмет";
        public override Delegate Delegate => (AddEquip)AddEquipCallback;

        [Inject]
        public void Inject(InventoryConfig inventoryConfig, IInventoryService inventoryService, ILocalizationService localization)
        {
            this.inventoryConfig = inventoryConfig;
            this.inventoryService = inventoryService;
            this.localization = localization;
        }

        private void AddEquipCallback(int dataId, int count)
        {
            var itemData = inventoryConfig.GetItem(dataId);

            if (itemData == null)
            {
                this.ErrorMsg($"Предмет с ID '{dataId}' не найден!");
                return;
            }

            var resultCount = 0;
            var totalCount = count;

            var itemsCount = itemData is IStackable stackable
                ? Mathf.CeilToInt(totalCount / (float)stackable.Stack.Size)
                : totalCount;

            for (var i = 0; i < itemsCount; i++)
            {
                // create item
                var newItem = inventoryService.Factory.CreateItem(dataId);
                var itemCount = 1;

                if (newItem is IStackableItem stackableItem)
                {
                    itemCount = Mathf.Min(totalCount, stackableItem.Stack.Size);

                    stackableItem.Stack.SetValue(itemCount);
                }

                totalCount -= itemCount;

                // find placement
                var placementArgs = new PlaceInPlacementArgs
                {
                    ItemId = newItem.Id
                };

                var hasPlacement = inventoryService.ItemsGrids.Any(grid =>
                {
                    placementArgs.PlacementId = grid.Id;

                    return inventoryService.CheckPossibilityOfPlacement(placementArgs);
                });

                if (hasPlacement == false)
                {
                    return;
                }

                // place
                inventoryService.TryPlaceItem(placementArgs);

                resultCount += itemCount;
            }

            if (resultCount > 0)
            {
                var itemName = localization.Get(itemData.NameKey);

                this.LogMsg($"Создан предмет '{itemName}' и помещен в хранилище [x{resultCount}]");
            }
            else
            {
                this.ErrorMsg($"Предмет с ID '{dataId}' не создан!");
            }
        }
    }
}