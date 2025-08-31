using AD.ToolsCollection;
using Game.GuildLevels;

namespace Game.Guild
{
    [Hidden, Menu("Guild: Request Time Mod")]
    [LevelMechanic(typeof(RequestTimeLevelMechanic))]
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