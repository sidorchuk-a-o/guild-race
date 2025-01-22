using AD.ToolsCollection;
using UnityEditor;
using UnityEngine.UIElements;

namespace Game.Craft
{
    [ConfigEditor(typeof(CraftConfig))]
    public class CraftConfigEditor : ConfigEditor
    {
        private VendorsList vendorsList;
        private ReagentsParamsEditor reagentsParamsEditor;
        private RecyclingParamsEditor recyclingParamsEditor;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Vendors", CreateVendorsTab);
            tabs.CreateTabs("Reagent Params", CreateReagentParamsTabs);
            tabs.CreateTabs("Recycling Params", CreateRecyclingParamsTabs);
        }

        private void CreateVendorsTab(VisualElement root, SerializedData data)
        {
            vendorsList = root.CreateElement<VendorsList>();
            vendorsList.BindProperty("vendors", data);
            vendorsList.FlexWidth(50);
        }

        private void CreateReagentParamsTabs(TabsContainer tabs)
        {
            reagentsParamsEditor = new ReagentsParamsEditor();
            reagentsParamsEditor.CreateTabs(tabs);
        }

        private void CreateRecyclingParamsTabs(TabsContainer tabs)
        {
            recyclingParamsEditor = new RecyclingParamsEditor();
            recyclingParamsEditor.CreateTabs(tabs);
        }

        // == Menu ==

        [MenuItem("Game Services/Game/Craft")]
        public static CraftConfigEditor GoToEditor()
        {
            return GoToEditor<CraftConfigEditor>(width: 1200, height: 900);
        }

        public static CraftConfigEditor GoToEditor(CraftConfig config)
        {
            return GoToEditor<CraftConfigEditor>(config, width: 1200, height: 900);
        }
    }
}