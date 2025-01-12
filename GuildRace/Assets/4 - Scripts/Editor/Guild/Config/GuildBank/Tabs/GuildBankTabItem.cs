using AD.Services.Localization;
using AD.ToolsCollection;
using Game.Inventory;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Guild
{
    /// <summary>
    /// Item: <see cref="GuildBankTabData"/>
    /// </summary>
    public class GuildBankTabItem : ListItemElement
    {
        private LocalizeKeyElement nameKeyField;
        private AddressableElement<Sprite> iconRefField;
        private ItemsGridElement gridEditor;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            root.ConvertToColumn();

            root.CreateHeader("View");

            nameKeyField = root.CreateKey<LocalizeKey, string>() as LocalizeKeyElement;
            nameKeyField.previewOn = true;

            iconRefField = root.CreateAddressable<Sprite>();

            gridEditor = root.CreateElement<ItemsGridElement>();
            gridEditor.label = "Bank Grid Params";
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