using AD.ToolsCollection;
using Game.GuildLevels;
using UnityEditor;

namespace Game.Quests
{
    [Hidden, Menu("Quests: Quest Count")]
    [LevelMechanic(typeof(QuestCountLevelMechanic))]
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