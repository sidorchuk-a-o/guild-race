using AD.ToolsCollection;

namespace Game.Quests
{
    /// <summary>
    /// Item: <see cref="QuestData"/>
    /// </summary>
    public class QuestItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => QuestsEditorState.EditorsFactory;
    }
}