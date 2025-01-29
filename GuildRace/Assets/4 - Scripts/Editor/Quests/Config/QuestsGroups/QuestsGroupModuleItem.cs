using AD.ToolsCollection;

namespace Game.Quests
{
    /// <summary>
    /// Item: <see cref="QuestsGroupModule"/>
    /// </summary>
    public class QuestsGroupModuleItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => QuestsEditorState.EditorsFactory;
    }
}