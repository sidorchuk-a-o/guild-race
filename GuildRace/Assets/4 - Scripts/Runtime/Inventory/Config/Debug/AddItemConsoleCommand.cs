using AD.Services.Debug;
using AD.Services.Localization;
using AD.ToolsCollection;
using Game.Inventory;
using System;
using VContainer;

namespace Game.Guild
{
    public class AddItemConsoleCommand : ConsoleCommandHandler
    {
        public delegate void AddEquip(int id);

        private IInventoryService inventoryService;
        private ILocalizationService localization;

        public override string Name => "add_item";
        public override string Desc => "Добавить предмет";
        public override Delegate Delegate => (AddEquip)AddEquipCallback;

        [Inject]
        public void Inject(IInventoryService inventoryService, ILocalizationService localization)
        {
            this.inventoryService = inventoryService;
            this.localization = localization;
        }

        private void AddEquipCallback(int id)
        {
            var newItem = inventoryService.Factory.CreateItem(id);
            var itemName = localization.Get(newItem.NameKey);

            foreach (var itemsGrid in inventoryService.ItemsGrids)
            {
                var placeArgs = new PlaceInPlacementArgs
                {
                    ItemId = newItem.Id,
                    PlacementId = itemsGrid.Id
                };

                if (inventoryService.CheckPossibilityOfPlacement(placeArgs))
                {
                    inventoryService.TryPlaceItem(placeArgs);

                    this.LogMsg($"Создан предмет '{itemName}' и помещен в хранилище");
                    return;
                }
            }

            this.ErrorMsg($"Предмет '{itemName}' не создан");
        }
    }
}