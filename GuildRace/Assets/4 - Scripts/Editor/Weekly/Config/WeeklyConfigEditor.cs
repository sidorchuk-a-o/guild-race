using AD.ToolsCollection;
using UnityEditor;
using UnityEngine.UIElements;

namespace Game.Weekly
{
    [ConfigEditor(typeof(WeeklyConfig))]
    public class WeeklyConfigEditor : ConfigEditor
    {
        private PropertyElement daysCountField;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Params", CreateParamsTab);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            daysCountField = root.CreateProperty();
            daysCountField.BindProperty("daysCount", data);
        }

        // == Menu ==

        [MenuItem("Game Services/Game/Weekly")]
        public static WeeklyConfigEditor GoToEditor()
        {
            return GoToEditor<WeeklyConfigEditor>();
        }

        public static WeeklyConfigEditor GoToEditor(WeeklyConfig config)
        {
            return GoToEditor<WeeklyConfigEditor>(config);
        }
    }
}