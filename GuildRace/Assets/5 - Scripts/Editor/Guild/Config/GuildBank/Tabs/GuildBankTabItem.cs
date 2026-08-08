using AD.Game.Inventory;
using AD.Localization;
using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace AD.Game.Guild
{
    public class GuildBankTabItem : ListItemElement<GuildBankTabData>
    {
        private LocalizeKeyElement nameKeyField;
        private AddressableElement<Sprite> iconRefField;
        private ItemsGridElement<ItemsGridBaseData> gridEditor;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            root.ConvertToColumn();

            root.CreateHeader("View");

            nameKeyField = (LocalizeKeyElement)root.CreateKeyPopup<LocalizeKey, string>();
            nameKeyField.PreviewOn = true;

            iconRefField = root.CreateAddressable<Sprite>();

            gridEditor = root.CreateElement<ItemsGridElement<ItemsGridBaseData>>();
            gridEditor.Label = "Bank Grid Params";
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            nameKeyField.BindProperty("nameKey", data);
            iconRefField.BindProperty("iconRef", data);
            gridEditor.BindProperty("grid", data);
        }
    }
}