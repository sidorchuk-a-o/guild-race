using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    public class UnitCooldownParamsItem : ListItemElement<UnitCooldownParams>
    {
        private PropertyElement instanceTypeField;
        private PropertyElement maxTriesCountField;
        private PropertyElement maxCompletedCountField;
        private PropertyElement isWeeklyResetField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            root.ConvertToColumn();

            base.CreateItemContentGUI(root);

            instanceTypeField = root.CreateProperty();
            maxTriesCountField = root.CreateProperty();
            maxCompletedCountField = root.CreateProperty();
            isWeeklyResetField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            instanceTypeField.BindProperty("instanceType", data);
            maxTriesCountField.BindProperty("maxTriesCount", data);
            maxCompletedCountField.BindProperty("maxCompletedCount", data);
            isWeeklyResetField.BindProperty("isWeeklyReset", data);
        }
    }
}