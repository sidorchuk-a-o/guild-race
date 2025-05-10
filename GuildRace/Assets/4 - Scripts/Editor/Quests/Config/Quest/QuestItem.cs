using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Quests
{
    /// <summary>
    /// Item: <see cref="QuestData"/>
    /// </summary>
    public class QuestItem : EntityListItemElement
    {
        private ItemIdElement idLabel;

        protected override IEditorsFactory EditorsFactory => QuestsEditorState.EditorsFactory;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            idLabel = root.CreateElement<ItemIdElement>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            idLabel.BindData(data);
        }
    }
}