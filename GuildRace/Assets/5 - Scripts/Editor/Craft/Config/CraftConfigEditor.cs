using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace AD.Game.Craft
{
    public class CraftConfigEditor : ConfigEditor<CraftConfig>
    {
        private VendorsList vendorsList;
        private ReagentsParamsEditor reagentsParamsEditor;
        private RecyclingParamsEditor recyclingParamsEditor;

        public override Vector2 Size { get; } = new(1200, 900);

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

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
            reagentsParamsEditor = new();
            reagentsParamsEditor.CreateTabs(tabs);
        }

        private void CreateRecyclingParamsTabs(TabsContainer tabs)
        {
            recyclingParamsEditor = new();
            recyclingParamsEditor.CreateTabs(tabs);
        }
    }
}