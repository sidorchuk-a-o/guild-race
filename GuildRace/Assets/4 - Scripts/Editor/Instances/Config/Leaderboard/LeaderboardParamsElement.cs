using AD.ToolsCollection;

namespace Game.Instances
{
    /// <summary>
    /// Element: <see cref="LeaderboardParams"/>
    /// </summary>
    public class LeaderboardParamsElement : Element
    {
        private PropertyElement guildScoreKeyField;
        private InstanceScoresList instanceScoresList;

        protected override void CreateElementGUI(Element root)
        {
            root.CreateHeader("Leaderboard Params");

            guildScoreKeyField = root.CreateProperty();

            root.CreateSpace();

            instanceScoresList = root.CreateElement<InstanceScoresList>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            guildScoreKeyField.BindProperty("guildScoreKey", data);
            instanceScoresList.BindProperty("instanceScores", data);
        }
    }
}