using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    [DataEditor(typeof(SquadData))]
    public class SquadEditor : DataEditor
    {
        private PropertyElement maxUnitsCountField;

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

            tabs.CreateTab(CreateTabs);
            tabs.Content.Width(50, LengthUnit.Percent);
        }

        private void CreateTabs(VisualElement root, SerializedData data)
        {
            maxUnitsCountField = root.CreateProperty();
            maxUnitsCountField.BindProperty("maxUnitsCount", data);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            var type = data.GetProperty("instanceType").GetValue<InstanceType>();
            var typeName = InstancesEditorState.Config.GetInstanceType(type).Title;

            Title = $"{typeName} Squad";
        }
    }
}