using AD.Game.GuildLevels;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    [Menu("Guild: Bank Row Count")]
    [DataEditor(typeof(BankRowCountLevelMechanic))]
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