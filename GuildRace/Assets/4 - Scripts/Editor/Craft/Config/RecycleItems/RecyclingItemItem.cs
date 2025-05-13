using AD.ToolsCollection;
using Game.Inventory;
using UnityEngine.UIElements;

namespace Game.Craft
{
    /// <summary>
    /// Item: <see cref="RecyclingItemData"/>
    /// </summary>
    public class RecyclingItemItem : ListItemElement
    {
        private KeyElement<int> rarityPopup;
        private PropertyElement percentField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            root.ConvertToColumn();

            rarityPopup = root.CreateKey<Rarity, int>();
            percentField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            rarityPopup.BindProperty("rarity", data);
            percentField.BindProperty("percent", data);
        }
    }
}