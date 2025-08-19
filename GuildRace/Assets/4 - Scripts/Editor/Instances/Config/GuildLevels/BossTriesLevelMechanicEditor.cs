using AD.ToolsCollection;
using Game.GuildLevels;

namespace Game.Instances
{
    [Hidden, Menu("Instances: Boss Tries")]
    [LevelMechanic(typeof(BossTriesLevelMechanic))]
    public class BossTriesLevelMechanicEditor : LevelMechanicEditor
    {
        private PropertyElement instanceTypeField;
        private PropertyElement increaseValueField;

        protected override void CreateElementGUI(Element root)
        {
            instanceTypeField = root.CreateProperty();
            increaseValueField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            instanceTypeField.BindProperty("instanceType", data);
            increaseValueField.BindProperty("increaseValue", data);
        }
    }
}