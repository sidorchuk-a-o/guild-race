using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    [InventoryEditor(typeof(EquipTypeData))]
    public class EquipTypeEditor : EntityEditor
    {
        private LocalizeKeyElement nameKeyField;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Common", CreateCommonTab);

            tabs.content.Width(50, LengthUnit.Percent);
        }

        private void CreateCommonTab(VisualElement root, SerializedData data)
        {
            nameKeyField = root.CreateKey<LocalizeKey, string>() as LocalizeKeyElement;
            nameKeyField.BindProperty("nameKey", data);
            nameKeyField.previewOn = true;
        }
    }
}