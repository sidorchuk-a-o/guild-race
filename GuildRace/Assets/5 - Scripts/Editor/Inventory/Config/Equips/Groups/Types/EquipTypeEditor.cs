using AD.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Inventory
{
    [DataEditor(typeof(EquipTypeData))]
    public class EquipTypeEditor : DataEditor
    {
        private LocalizeKeyElement nameKeyField;

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

            tabs.CreateTab("Common", CreateCommonTab);
            tabs.Content.Width(50, LengthUnit.Percent);
        }

        private void CreateCommonTab(VisualElement root, SerializedData data)
        {
            nameKeyField = (LocalizeKeyElement)root.CreateKeyPopup<LocalizeKey, string>();
            nameKeyField.BindProperty("nameKey", data);
            nameKeyField.PreviewOn = true;
        }
    }
}