using AD.ToolsCollection;
using UnityEditor;

namespace Game.Items
{
    [ConfigEditor(typeof(ItemsDatabaseConfig))]
    public class ItemsDatabaseConfigEditor : ConfigEditor
    {
        private EquipsParamsEditor equipsParamsEditor;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTabs("Equips", CreateEquipsTab);
            tabs.CreateTabs("Other", CreateOtherTab);
        }

        private void CreateEquipsTab(TabsContainer tabs)
        {
            equipsParamsEditor = new EquipsParamsEditor();
            equipsParamsEditor.CreateTabs(tabs);
        }

        private void CreateOtherTab(TabsContainer tabs)
        {
        }

        // == Menu ==

        [MenuItem("Game Services/Game/Items Database")]
        public static ItemsDatabaseConfigEditor GoToEditor()
        {
            return GoToEditor<ItemsDatabaseConfigEditor>(width: 1200, height: 900);
        }

        public static ItemsDatabaseConfigEditor GoToEditor(ItemsDatabaseConfig config)
        {
            return GoToEditor<ItemsDatabaseConfigEditor>(config, width: 1200, height: 900);
        }
    }
}