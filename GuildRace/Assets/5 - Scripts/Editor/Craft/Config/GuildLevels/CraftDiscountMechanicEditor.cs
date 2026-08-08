using AD.Game.GuildLevels;
using AD.ToolsCollection;

namespace AD.Game.Craft
{
    [Menu("Craft: Discount")]
    [DataEditor(typeof(CraftDiscountMechanic))]
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