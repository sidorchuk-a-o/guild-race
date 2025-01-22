using AD.ToolsCollection;
using Game.Inventory;
using UnityEngine.UIElements;

namespace Game.Craft
{
    /// <summary>
    /// Item: <see cref="RecyclingRarityModData"/>
    /// </summary>
    public class RecyclingRarityModItem : ListItemElement
    {
        private PropertyElement valueField;
        private KeyElement<int> rarityPopup;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            root.ConvertToRow();

            valueField = root.CreateProperty();
            valueField.Width(60);
            valueField.labelOn = false;

            rarityPopup = root.CreateKey<Rarity, int>();
            rarityPopup.FlexGrow(1);
            rarityPopup.filterOn = false;
            rarityPopup.labelOn = false;
            rarityPopup.removeOn = false;
            rarityPopup.updateOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            valueField.BindProperty("value", data);
            rarityPopup.BindProperty("rarity", data);
        }
    }
}