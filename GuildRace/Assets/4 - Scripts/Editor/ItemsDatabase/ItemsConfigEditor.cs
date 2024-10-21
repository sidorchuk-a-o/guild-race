using AD.ToolsCollection;
using UnityEditor;

namespace Game.Items
{
    [ConfigEditor(typeof(ItemsConfig))]
    public class ItemsConfigEditor : ConfigEditor
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
        public static ItemsConfigEditor GoToEditor()
        {
            return GoToEditor<ItemsConfigEditor>(width: 1200, height: 900);
        }

        public static ItemsConfigEditor GoToEditor(ItemsConfig config)
        {
            return GoToEditor<ItemsConfigEditor>(config, width: 1200, height: 900);
        }
    }
}