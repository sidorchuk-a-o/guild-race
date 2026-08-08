using AD.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Inventory
{
    [DataEditor(typeof(RarityData))]
    public class RarityEditor : DataEditor
    {
        private LocalizeKeyElement nameKeyField;
        private PropertyElement colorField;

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

            tabs.CreateTab("Params", CreateParamsTab);
            tabs.Content.Width(50, LengthUnit.Percent);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            nameKeyField = (LocalizeKeyElement)root.CreateKeyPopup<LocalizeKey, string>();
            nameKeyField.BindProperty("nameKey", data);
            nameKeyField.PreviewOn = true;

            colorField = root.CreateProperty();
            colorField.BindProperty("color", data);
        }
    }
}