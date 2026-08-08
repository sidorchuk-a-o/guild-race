using AD.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    [DataEditor(typeof(SeasonData))]
    public class SeasonEditor : DataEditor
    {
        private LocalizeKeyElement nameKeyField;
        private LocalizeKeyElement descKeyField;
        private InstancesList instancesList;

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

            descKeyField = (LocalizeKeyElement)root.CreateKeyPopup<LocalizeKey, string>();
            descKeyField.BindProperty("descKey", data);
            descKeyField.PreviewOn = true;

            root.CreateHeader("Instances");

            instancesList = root.CreateElement<InstancesList>();
            instancesList.BindProperty("instances", data);
        }
    }
}