using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    [InstancesEditor(typeof(SquadData))]
    public class SquadEditor : Editor
    {
        private PropertyElement maxUnitsCountField;
        private SquadRolesList rolesList;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab(CreateTabs);
            tabs.content.Width(50, LengthUnit.Percent);
        }

        private void CreateTabs(VisualElement root, SerializedData data)
        {
            maxUnitsCountField = root.CreateProperty();
            maxUnitsCountField.BindProperty("maxUnitsCount", data);

            rolesList = root.CreateElement<SquadRolesList>();
            rolesList.BindProperty("roles", data);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            var type = data.GetProperty("instanceType").GetValue<InstanceType>();
            var typeName = InstancesEditorState.Config.GetInstanceType(type).Title;

            title = $"{typeName} Squad";
        }
    }
}