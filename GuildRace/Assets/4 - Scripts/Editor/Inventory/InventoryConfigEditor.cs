using AD.ToolsCollection;
using UnityEditor;

namespace Game.Inventory
{
    [ConfigEditor(typeof(InventoryConfig))]
    public class InventoryConfigEditor : ConfigEditor
    {
        private EquipsParamsEditor equipsParamsEditor;
        private ItemsParamsEditor itemsParamsEditor;
        private ItemSlotsParamsEditor itemSlotsParamsEditor;
        private ItemsGridsParamsEditor itemsGridsParamsEditor;
        private UIParamsEditor uiParamsEditor;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTabs("Equips", CreateEquipsTab);
            tabs.CreateTabs("Items", CreateItemsTab);
            tabs.CreateTabs("Items Slots", CreateItemSlotsTab);
            tabs.CreateTabs("Items Grids", CreateItemsGridsTab);
            tabs.CreateTabs("UI", CreateUITab);
        }

        private void CreateEquipsTab(TabsContainer tabs)
        {
            equipsParamsEditor = new EquipsParamsEditor();
            equipsParamsEditor.CreateTabs(tabs);
        }

        private void CreateItemsTab(TabsContainer tabs)
        {
            itemsParamsEditor = new ItemsParamsEditor();
            itemsParamsEditor.CreateTabs(tabs);
        }

        private void CreateItemSlotsTab(TabsContainer tabs)
        {
            itemSlotsParamsEditor = new ItemSlotsParamsEditor();
            itemSlotsParamsEditor.CreateTabs(tabs);
        }

        private void CreateItemsGridsTab(TabsContainer tabs)
        {
            itemsGridsParamsEditor = new ItemsGridsParamsEditor();
            itemsGridsParamsEditor.CreateTabs(tabs);
        }

        private void CreateUITab(TabsContainer tabs)
        {
            uiParamsEditor = new UIParamsEditor();
            uiParamsEditor.CreateTabs(tabs);
        }

        // == Menu ==

        [MenuItem("Game Services/Game/Inventory")]
        public static InventoryConfigEditor GoToEditor()
        {
            return GoToEditor<InventoryConfigEditor>(width: 1200, height: 900);
        }

        public static InventoryConfigEditor GoToEditor(InventoryConfig config)
        {
            return GoToEditor<InventoryConfigEditor>(config, width: 1200, height: 900);
        }
    }
}