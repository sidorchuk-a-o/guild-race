using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    [InstancesEditor(typeof(SeasonData))]
    public class SeasonEditor : EntityEditor
    {
        private LocalizeKeyElement nameKeyField;
        private LocalizeKeyElement descKeyField;
        private InstancesList instancesList;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Params", CreateCommonTab);

            tabs.content.Width(50, LengthUnit.Percent);
        }

        protected virtual void CreateCommonTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("View");

            nameKeyField = root.CreateKey<LocalizeKey, string>() as LocalizeKeyElement;
            nameKeyField.BindProperty("nameKey", data);
            nameKeyField.previewOn = true;

            descKeyField = root.CreateKey<LocalizeKey, string>() as LocalizeKeyElement;
            descKeyField.BindProperty("descKey", data);
            descKeyField.previewOn = true;

            root.CreateHeader("Instances");

            instancesList = root.CreateElement<InstancesList>();
            instancesList.BindProperty("instances", data);
        }
    }
}