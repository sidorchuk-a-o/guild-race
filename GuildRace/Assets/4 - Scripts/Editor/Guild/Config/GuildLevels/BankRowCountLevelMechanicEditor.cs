using AD.ToolsCollection;
using Game.GuildLevels;

namespace Game.Guild
{
    [Hidden, Menu("Guild: Bank Row Count")]
    [LevelMechanic(typeof(BankRowCountLevelMechanic))]
    public class BankRowCountLevelMechanicEditor : LevelMechanicEditor
    {
        private PropertyElement increaseValueField;

        protected override void CreateElementGUI(Element root)
        {
            increaseValueField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            increaseValueField.BindProperty("increaseValue", data);
        }
    }
}