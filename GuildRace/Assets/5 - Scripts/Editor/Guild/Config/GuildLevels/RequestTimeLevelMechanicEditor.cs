using AD.Game.GuildLevels;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    [Menu("Guild: Request Time Mod")]
    [DataEditor(typeof(RequestTimeLevelMechanic))]
    public class RequestTimeLevelMechanicEditor : LevelMechanicEditor
    {
        private PropertyElement decreasePercentField;

        protected override void CreateElementGUI(Element root)
        {
            decreasePercentField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            decreasePercentField.BindProperty("decreasePercent", data);
        }
    }
}