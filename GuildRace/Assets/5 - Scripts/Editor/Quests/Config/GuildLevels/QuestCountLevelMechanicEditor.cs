using AD.Game.GuildLevels;
using AD.ToolsCollection;

namespace AD.Game.Quests
{
    [Menu("Quests: Quest Count")]
    [DataEditor(typeof(QuestCountLevelMechanic))]
    public class QuestCountLevelMechanicEditor : LevelMechanicEditor
    {
        private PropertyElement questsGroupField;
        private PropertyElement increaseValueField;

        protected override void CreateElementGUI(Element root)
        {
            questsGroupField = root.CreateProperty();
            increaseValueField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            questsGroupField.BindProperty("questsGroup", data);
            increaseValueField.BindProperty("increaseValue", data);
        }
    }
}