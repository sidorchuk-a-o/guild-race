using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Instances
{
    /// <summary>
    /// Item: <see cref="SeasonData"/>
    /// </summary>
    public class SeasonItem : EntityListItemElement
    {
        private LabelElement idLabel;

        protected override IEditorsFactory EditorsFactory => InstancesEditorState.EditorsFactory;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            idLabel = root.CreateElement<LabelElement>(classNames: ClassNames.stretchCell);
            idLabel.textElement.TextAnchor(TextAnchor.MiddleRight);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            var id = data.GetProperty("id").GetValue();

            idLabel.text = id.ToString();
        }
    }
}