using AD.ToolsCollection;
using Game.GuildLevels;

namespace Game.Guild
{
    [Hidden, Menu("Guild: Characters Count Increase")]
    [LevelMechanic(typeof(CharactersCountLevelMechanic))]
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