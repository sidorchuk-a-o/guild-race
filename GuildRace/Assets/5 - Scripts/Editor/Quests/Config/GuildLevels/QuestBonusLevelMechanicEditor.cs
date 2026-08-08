using AD.Game.GuildLevels;
using AD.ToolsCollection;

namespace AD.Game.Quests
{
    [Menu("Quests: Reward Bonus")]
    [DataEditor(typeof(QuestBonusLevelMechanic))]
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