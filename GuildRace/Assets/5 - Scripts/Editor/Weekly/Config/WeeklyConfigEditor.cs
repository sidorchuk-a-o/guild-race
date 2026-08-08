using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Weekly
{
    public class WeeklyConfigEditor : ConfigEditor<WeeklyConfig>
    {
        private PropertyElement daysCountField;

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

            tabs.CreateTab("Params", CreateParamsTab);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            daysCountField = root.CreateProperty();
            daysCountField.BindProperty("daysCount", data);
        }
    }
}