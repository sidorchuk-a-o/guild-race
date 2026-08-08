using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game
{
    public class ItemIdElement : LabelElement
    {
        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            this.FlexGrow(0);

            AddToClassList("elements-list__cell--stretch");

            TextElement.TextAnchor(TextAnchor.MiddleRight);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            var id = data.GetProperty("id").GetValue();

            Text = id.ToString();
        }
    }
}