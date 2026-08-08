using AD.Game.GuildLevels;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    [Menu("Guild: Characters Count Increase")]
    [DataEditor(typeof(CharactersCountLevelMechanic))]
    public class CharactersCountLevelMechanicEditor : LevelMechanicEditor
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