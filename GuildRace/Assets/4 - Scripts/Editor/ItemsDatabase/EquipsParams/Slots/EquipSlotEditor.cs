using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Items
{
    [ItemsEditor(typeof(EquipSlotData))]
    public class EquipSlotEditor : EntityEditor
    {
        private KeyElement nameKeyField;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Common", CreateCommonTab);

            tabs.content.Width(50, LengthUnit.Percent);
        }

        private void CreateCommonTab(VisualElement root, SerializedData data)
        {
            nameKeyField = root.CreateKey<LocalizeKey>();
            nameKeyField.BindProperty("nameKey", data);
        }
    }
}