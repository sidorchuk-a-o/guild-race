using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    /// <summary>
    /// Item: <see cref="SubRoleThreatData"/>
    /// </summary>
    public class SubRoleThreatItem : ListItemElement
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