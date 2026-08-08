using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace AD.Game.GuildLevels
{
    public class LevelItem : ListItemElement<LevelData>
    {
        private LabelElement indexLabel;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            indexLabel = root.CreateElement<LabelElement>();

            indexLabel
                .FontStyle(FontStyle.Bold)
                .FontSize(14)
                .PaddingLeft(5);

            base.CreateItemContentGUI(root);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            indexLabel.Text = $"[ {Index + 1} ]";
        }
    }
}