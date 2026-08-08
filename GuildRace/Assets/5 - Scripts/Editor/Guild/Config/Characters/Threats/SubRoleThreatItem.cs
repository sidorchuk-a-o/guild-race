using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Guild
{
    public class SubRoleThreatItem : ListItemElement<SubRoleThreatData>
    {
        private PropertyElement subRoleIdField;
        private PropertyElement threatIdField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            root.ConvertToColumn();

            base.CreateItemContentGUI(root);

            subRoleIdField = root.CreateProperty();
            threatIdField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            subRoleIdField.BindProperty("subRoleId", data);
            threatIdField.BindProperty("threatId", data);
        }
    }
}