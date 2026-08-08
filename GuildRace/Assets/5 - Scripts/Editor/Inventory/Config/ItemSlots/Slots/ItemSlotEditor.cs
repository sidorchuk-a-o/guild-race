using AD.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Inventory
{
    /// <summary>
    ///     Editor: <see cref="ItemSlotData" />
    /// </summary>
    public abstract class ItemSlotEditor : DataEditor
    {
        private KeyElement<string> nameKeyField;

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

            tabs.CreateTab("Params", CreateCommonTab);
            tabs.Content.Width(50, LengthUnit.Percent);
        }

        protected virtual void CreateCommonTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("View");

            nameKeyField = root.CreateKeyPopup<LocalizeKey, string>();
            nameKeyField.BindProperty("nameKey", data);
        }
    }
}