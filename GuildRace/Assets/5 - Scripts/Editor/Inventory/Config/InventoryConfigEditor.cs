using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Inventory
{
    public class InventoryConfigEditor : ConfigEditor<InventoryConfig>
    {
        private EquipsParamsEditor equipsParamsEditor;
        private ItemsParamsEditor itemsParamsEditor;
        private ItemSlotsParamsEditor itemSlotsParamsEditor;
        private ItemsGridsParamsEditor itemsGridsParamsEditor;
        private UIParamsEditor uiParamsEditor;

        public override Vector2 Size { get; } = new(1200, 900);

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

            tabs.CreateTabs("Equips", CreateEquipsTab);
            tabs.CreateTabs("Items", CreateItemsTab);
            tabs.CreateTabs("Items Slots", CreateItemSlotsTab);
            tabs.CreateTabs("Items Grids", CreateItemsGridsTab);
            tabs.CreateTabs("UI", CreateUITab);
        }

        private void CreateEquipsTab(TabsContainer tabs)
        {
            equipsParamsEditor = new();
            equipsParamsEditor.CreateTabs(tabs);
        }

        private void CreateItemsTab(TabsContainer tabs)
        {
            itemsParamsEditor = new();
            itemsParamsEditor.CreateTabs(tabs);
        }

        private void CreateItemSlotsTab(TabsContainer tabs)
        {
            itemSlotsParamsEditor = new();
            itemSlotsParamsEditor.CreateTabs(tabs);
        }

        private void CreateItemsGridsTab(TabsContainer tabs)
        {
            itemsGridsParamsEditor = new();
            itemsGridsParamsEditor.CreateTabs(tabs);
        }

        private void CreateUITab(TabsContainer tabs)
        {
            uiParamsEditor = new();
            uiParamsEditor.CreateTabs(tabs);
        }
    }
}