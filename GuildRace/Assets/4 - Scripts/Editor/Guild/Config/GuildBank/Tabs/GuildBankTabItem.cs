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
        private PropertyElement rowSizeField;
        private PropertyElement rowCountField;
        private KeyElement<int> categoryField;
        private KeyElement<int> cellTypeField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            root.ConvertToColumn();

            root.CreateHeader("View");
            nameKeyField = root.CreateKey<LocalizeKey, string>() as LocalizeKeyElement;
            nameKeyField.previewOn = true;

            iconRefField = root.CreateAddressable<Sprite>();

            root.CreateHeader("Params");
            rowSizeField = root.CreateProperty();
            rowCountField = root.CreateProperty();
            categoryField = root.CreateKey<ItemsGridCategory, int>();
            cellTypeField = root.CreateKey<ItemsGridCellType, int>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            TryCreateGrid(data, out var gridProp);

            nameKeyField.BindProperty("nameKey", data);
            iconRefField.BindProperty("iconRef", data);
            rowSizeField.BindProperty("rowSize", gridProp);
            rowCountField.BindProperty("rowCount", gridProp);
            categoryField.BindProperty("category", gridProp);
            cellTypeField.BindProperty("cellType", gridProp);
        }

        private void TryCreateGrid(SerializedData data, out SerializedData gridProp)
        {
            gridProp = data.GetProperty("grid");

            if (gridProp.GetValue() == null)
            {
                var saveMeta = new SaveMeta(isSubObject: true, data);
                var gridData = DataFactory.Create<ItemsGridData>(saveMeta);

                gridProp.SetValue(gridData);
            }
        }
    }
}