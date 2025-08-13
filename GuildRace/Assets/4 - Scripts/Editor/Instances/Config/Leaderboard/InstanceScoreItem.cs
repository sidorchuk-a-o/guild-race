using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    /// <summary>
    /// Item: <see cref="InstanceScoreData"/>
    /// </summary>
    public class InstanceScoreItem : ListItemElement
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