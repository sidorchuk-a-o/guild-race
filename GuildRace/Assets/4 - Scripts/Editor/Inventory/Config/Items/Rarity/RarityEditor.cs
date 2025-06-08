using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    [InventoryEditor(typeof(RarityData))]
    public class RarityEditor : EntityEditor
    {
        private LocalizeKeyElement nameKeyField;
        private PropertyElement colorField;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Params", CreateParamsTab);

            tabs.content.Width(50, LengthUnit.Percent);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            nameKeyField = root.CreateKey<LocalizeKey, string>() as LocalizeKeyElement;
            nameKeyField.BindProperty("nameKey", data);
            nameKeyField.previewOn = true;

            colorField = root.CreateProperty();
            colorField.BindProperty("color", data);
        }
    }
}