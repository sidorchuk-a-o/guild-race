using AD.ToolsCollection;
using UnityEngine;

namespace Game
{
    public class ItemIdElement : LabelElement
    {
        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            this.FlexGrow(0);

            AddToClassList("elements-list__cell--stretch");

            textElement.TextAnchor(TextAnchor.MiddleRight);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            var id = data.GetProperty("id").GetValue();

            text = id.ToString();
        }
    }
}