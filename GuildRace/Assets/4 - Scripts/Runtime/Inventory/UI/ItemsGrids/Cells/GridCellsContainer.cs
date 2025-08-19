using AD.ToolsCollection;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    public class GridCellsContainer : MonoBehaviour
    {
        [SerializeField] private GridCell cellPrefab;

        private readonly List<GridCell> cells = new();

        public void Init(ItemsGridVM gridVM, int cellSize)
        {
            var i = 0;

            var gridBounds = gridVM.Bounds.Value;
            var cellsCount = gridBounds.size.x * gridBounds.size.y;

            for (var h = 0; h < gridBounds.size.y; h++)
            {
                for (var w = 0; w < gridBounds.size.x; w++, i++)
                {
                    var cell = cells.ElementAtOrDefault(i);

                    var hasCell = cell != null;
                    var hasVM = i < cellsCount;

                    if (hasVM)
                    {
                        if (!hasCell)
                        {
                            cell = Instantiate(cellPrefab, transform);
                            cells.Add(cell);
                        }

                        var cellRect = cell.transform as RectTransform;

                        cellRect.anchoredPosition = new()
                        {
                            x = w * cellSize,
                            y = -h * cellSize
                        };

                        cell.SetActive(true);
                    }
                    else if (hasCell)
                    {
                        cell.SetActive(false);
                    }
                }
            }

            for (; i < cells.Count; i++)
            {
                cells[i].SetActive(false);
            }
        }
    }
}