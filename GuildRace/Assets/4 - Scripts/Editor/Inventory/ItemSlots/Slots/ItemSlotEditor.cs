using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Editor: <see cref="ItemSlotData"/>
    /// </summary>
    public abstract class ItemSlotEditor : EntityEditor
    {
        private KeyElement nameKeyField;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Params", CreateCommonTab);

            tabs.content.Width(50, LengthUnit.Percent);
        }

        protected virtual void CreateCommonTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("View");

            nameKeyField = root.CreateKey<LocalizeKey>();
            nameKeyField.BindProperty("nameKey", data);
        }
    }
}