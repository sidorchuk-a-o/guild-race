using AD.ToolsCollection;
using Game.GuildLevels;
using UnityEditor;

namespace Game.Quests
{
    [Hidden, Menu("Quests: Reward Bonus")]
    [LevelMechanic(typeof(QuestBonusLevelMechanic))]
    public class QuestBonusLevelMechanicEditor : LevelMechanicEditor
    {
        private PropertyElement bonusPercentField;

        protected override void CreateElementGUI(Element root)
        {
            bonusPercentField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            bonusPercentField.BindProperty("bonusPercent", data);
        }
    }
}