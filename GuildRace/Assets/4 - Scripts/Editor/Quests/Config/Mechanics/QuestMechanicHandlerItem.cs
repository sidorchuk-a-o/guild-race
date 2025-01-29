using AD.ToolsCollection;

namespace Game.Quests
{
    /// <summary>
    /// Item: <see cref="QuestMechanicHandlerData"/>
    /// </summary>
    public class QuestMechanicHandlerItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => QuestsEditorState.EditorsFactory;
    }
}