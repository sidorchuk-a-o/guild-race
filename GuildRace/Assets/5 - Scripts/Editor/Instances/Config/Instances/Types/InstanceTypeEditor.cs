using AD.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    [DataEditor(typeof(InstanceTypeData))]
    public class InstanceTypeEditor : DataEditor
    {
        private LocalizeKeyElement nameKeyField;

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

            tabs.CreateTab("Params", CreateCommonTab);
            tabs.Content.Width(50, LengthUnit.Percent);
        }

        protected virtual void CreateCommonTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("View");

            nameKeyField = (LocalizeKeyElement)root.CreateKeyPopup<LocalizeKey, string>();
            nameKeyField.BindProperty("nameKey", data);
            nameKeyField.PreviewOn = true;
        }
    }
}