using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.GuildLevels
{
    /// <summary>
    /// Item: <see cref="LevelData"/>
    /// </summary>
    public class LevelItem : EntityListItemElement
    {
        private LabelElement indexLabel;

        protected override IEditorsFactory EditorsFactory => GuildLevelsEditorState.EditorsFactory;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            indexLabel = root.CreateElement<LabelElement>();

            indexLabel
                .FontStyle(UnityEngine.FontStyle.Bold)
                .FontSize(14)
                .PaddingLeft(5);

            base.CreateItemContentGUI(root);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            indexLabel.text = $"[ {index + 1} ]";
        }
    }
}