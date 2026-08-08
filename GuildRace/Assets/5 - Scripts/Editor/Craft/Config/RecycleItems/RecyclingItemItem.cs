using AD.Game.Inventory;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Craft
{
    public class RecyclingItemItem : ListItemElement<RecyclingItemData>
    {
        private KeyElement<int> rarityPopup;
        private PropertyElement percentField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            root.ConvertToColumn();

            rarityPopup = root.CreateKeyPopup<Rarity, int>();
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