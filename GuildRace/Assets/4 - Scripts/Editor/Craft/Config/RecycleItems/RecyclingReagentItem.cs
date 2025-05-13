using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Craft
{
    /// <summary>
    /// Item: <see cref="RecyclingReagentData"/>
    /// </summary>
    public class RecyclingReagentItem : ListItemElement
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