using AD.ToolsCollection;
using System.Collections.Generic;

namespace Game.Inventory
{
    public class ItemsGridsList : ListElement<ItemsGridData, ItemsGridItem>
    {
        protected override List<Header> Headers => new()
        {
            new Header("RowSize", 80),
            new Header("RowCount", 80),
            new Header("Category", 168),
            new Header("Cell Type")
        };
    }
}