using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Inventory
{
    /// <summary>
    ///     Item: <see cref="ItemsGridData" />
    /// </summary>
    public class ItemsGridElement<TData> : Element
        where TData : ItemsGridData
    {
        private Label headerLabel;

        private PropertyElement rowSizeField;
        private PropertyElement rowCountField;
        private KeyElement<int> categoryField;
        private KeyElement<int> cellTypeField;

        public string Label
        {
            get => headerLabel.text;
            set => headerLabel.text = value;
        }

        protected override void CreateElementGUI(Element root)
        {
            root.CreateHeader("Items Grid Params", out headerLabel);

            rowSizeField = root.CreateProperty();
            rowCountField = root.CreateProperty();

            categoryField = root.CreateKeyPopup<ItemsGridCategory, int>();
            categoryField.FilterOn = false;
            categoryField.RemoveOn = false;

            cellTypeField = root.CreateKeyPopup<ItemsGridCellType, int>();
            cellTypeField.FilterOn = false;
            cellTypeField.RemoveOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            TryCreateGrid(data);

            rowSizeField.BindProperty("rowSize", data);
            rowCountField.BindProperty("rowCount", data);
            categoryField.BindProperty("category", data);
            cellTypeField.BindProperty("cellType", data);
        }

        private static void TryCreateGrid(SerializedData data)
        {
            var dataType = typeof(TData);

            if (data.GetValue() == null)
            {
                var saveMeta = new SaveMeta(isSubObject: true, data);
                var gridData = DataFactory.CreateInstance(dataType, saveMeta);

                data.SetValue(gridData);
            }
        }
    }
}