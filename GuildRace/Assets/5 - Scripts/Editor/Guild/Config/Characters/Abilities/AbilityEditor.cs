using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Guild
{
    [DataEditor(typeof(AbilityData))]
    public class AbilityEditor : DataEditor
    {
        private PropertyElement nameKeyField;
        private PropertyElement descKeyField;
        private PropertyElement threatIdField;

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

            tabs.CreateTab("Params", CreateParamsTab);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("View");

            nameKeyField = root.CreateProperty();
            nameKeyField.BindProperty("nameKey", data);

            descKeyField = root.CreateProperty();
            descKeyField.BindProperty("descKey", data);

            threatIdField = root.CreateProperty();
            threatIdField.BindProperty("threatId", data);
        }
    }
}