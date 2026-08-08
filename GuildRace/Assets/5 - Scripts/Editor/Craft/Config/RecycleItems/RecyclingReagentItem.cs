using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Craft
{
    public class RecyclingReagentItem : ListItemElement<RecyclingReagentData>
    {
        private PropertyElement rarityField;
        private PropertyElement currencyKeyField;
        private PropertyElement amountField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            root.ConvertToColumn();

            rarityField = root.CreateProperty();
            currencyKeyField = root.CreateProperty();
            amountField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            rarityField.BindProperty("rarity", data);
            currencyKeyField.BindProperty("currencyKey", data);
            amountField.BindProperty("amount", data);
        }
    }
}