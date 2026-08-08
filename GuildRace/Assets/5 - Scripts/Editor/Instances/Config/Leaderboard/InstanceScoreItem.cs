using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    public class InstanceScoreItem : ListItemElement<InstanceScoreData>
    {
        private PropertyElement typeField;
        private PropertyElement scoreField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            root.ConvertToColumn();

            typeField = root.CreateProperty();
            scoreField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            typeField.BindProperty("type", data);
            scoreField.BindProperty("score", data);
        }
    }
}