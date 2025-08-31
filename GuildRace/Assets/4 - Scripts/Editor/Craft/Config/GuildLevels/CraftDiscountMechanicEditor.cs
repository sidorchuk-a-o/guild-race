using AD.ToolsCollection;
using Game.GuildLevels;
using UnityEditor;

namespace Game.Craft
{
    [Hidden, Menu("Craft: Discount")]
    [LevelMechanic(typeof(CraftDiscountMechanic))]
    public class CraftDiscountMechanicEditor : LevelMechanicEditor
    {
        private PropertyElement discountValueField;

        protected override void CreateElementGUI(Element root)
        {
            discountValueField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            discountValueField.BindProperty("discountValue", data);
        }
    }
}