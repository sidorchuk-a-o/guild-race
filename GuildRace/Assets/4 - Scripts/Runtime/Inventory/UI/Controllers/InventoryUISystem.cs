using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UnityEngine;
using AD.UI;
using System.Threading;

namespace Game.Inventory
{
    public class InventoryUISystem : UIContainer
    {
        [Header("Controllers")]
        [SerializeField] private InventoryDraggableController draggableController;
        [SerializeField] private InventoryHighlightController highlightController;
        [SerializeField] private InventoryOptionsController optionsController;
        [SerializeField] private InventoryWindowsController windowsController;
        [SerializeField] private InventoryScrollController scrollController;

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            draggableController.Init(disp);
            highlightController.Init(disp);
            optionsController.Init(disp);
            windowsController.Init(disp);
            scrollController.Init(disp);
        }
    }
}