#pragma warning disable IDE1006 // Naming Styles

using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Item: <see cref="ItemsGridData"/>
    /// </summary>
    public class ItemsGridElement : Element
    {
        private Label m_Label;

        private PropertyElement rowSizeField;
        private PropertyElement rowCountField;
        private KeyElement<int> categoryField;
        private KeyElement<int> cellTypeField;

        public string label
        {
            get => m_Label.text;
            set => m_Label.text = value;
        }

        protected override void CreateElementGUI(Element root)
        {
            root.CreateHeader("Items Grid Params", out m_Label);

            rowSizeField = root.CreateProperty();
            rowCountField = root.CreateProperty();

            categoryField = root.CreateKey<ItemsGridCategory, int>();
            categoryField.filterOn = false;
            categoryField.removeOn = false;

            cellTypeField = root.CreateKey<ItemsGridCellType, int>();
            cellTypeField.filterOn = false;
            cellTypeField.removeOn = false;
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

        private void TryCreateGrid(SerializedData data)
        {
            if (data.GetValue() == null)
            {
                var saveMeta = new SaveMeta(isSubObject: true, data);
                var gridData = DataFactory.Create<ItemsGridData>(saveMeta);

                data.SetValue(gridData);
            }
        }
    }
}