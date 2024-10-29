using AD.Services.Pools;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

namespace Game.Inventory
{
    public class InventoryWindowsController : MonoBehaviour
    {
        [SerializeField] private int maxWindowsCount = 4;
        [SerializeField] private float newWindowOffset = 50f;
        [Space]
        [SerializeField] private RectTransform windowsParentRect;

        private readonly Dictionary<string, List<WindowContainer>> windowsDict = new();

        private InventoryVMFactory inventoryVMF;

        [Inject]
        public void Inject(InventoryVMFactory inventoryVMF)
        {
            this.inventoryVMF = inventoryVMF;
        }

        private void OnDisable()
        {
            CloseAllWindows();
        }

        public void Init(CompositeDisp disp)
        {
            MessageBroker.Default
                .Receive<OpenItemWindowArgs>()
                .Subscribe(args => OpenWindowCallback(args, disp))
                .AddTo(disp);

            WindowContainer.OnClosed
                .Subscribe(WindowOnClosedCallback)
                .AddTo(disp);
        }

        public static void OpenWindow(OpenItemWindowArgs args)
        {
            MessageBroker.Default.Publish(args);
        }

        private async void OpenWindowCallback(OpenItemWindowArgs args, CompositeDisp disp)
        {
            // find exist window
            if (FindExistWindow(args, out var existWindow))
            {
                existWindow.transform.SetAsLastSibling();
                return;
            }

            // create window
            var windowRef = args.WindowRef;

            var windowGO = await inventoryVMF.RentObjectAsync(windowRef);
            var window = windowGO.GetComponent<WindowContainer>();

            if (!windowsDict.TryGetValue(windowRef.AssetGUID, out var windows))
            {
                windows = ListPool<WindowContainer>.Get();

                windowsDict[windowRef.AssetGUID] = windows;
            }

            windows.Add(window);

            // init
            await window.Init(args.ItemId, disp);

            // upd rect
            var windowRT = window.transform as RectTransform;
            var windowRect = windowRT.rect;
            var activeWindowIndex = windowsParentRect.childCount - 1;

            windowRT.SetParent(windowsParentRect);
            windowRT.SetAsLastSibling();

            if (activeWindowIndex >= 0)
            {
                var activeWindow = windowsParentRect.GetChild(activeWindowIndex);

                var activeWindowRT = activeWindow as RectTransform;
                var activeWindowRect = activeWindowRT.rect;
                var activeWindowPosition = activeWindowRT.anchoredPosition;

                var activeTopLeftCorner = new Vector2(activeWindowRect.xMin, activeWindowRect.yMax);
                var windowSizeOffset = windowRT.sizeDelta * new Vector2(windowRT.pivot.x, -windowRT.pivot.y);
                var windowOffset = new Vector2(1, -1) * newWindowOffset;

                windowRT.anchoredPosition = activeWindowPosition + activeTopLeftCorner + windowOffset + windowSizeOffset;
                windowRT.ClampPositionToParent();
            }
            else
            {
                windowRT.anchoredPosition = Vector2.zero;
            }

            // close old window
            if (windows.Count > maxWindowsCount)
            {
                var oldWindow = windows
                    .OrderBy(x => x.transform.GetSiblingIndex())
                    .First();

                CloseWindow(oldWindow);

                windows.Remove(oldWindow);
            }
        }

        private bool FindExistWindow(OpenItemWindowArgs args, out WindowContainer window)
        {
            window = null;

            if (windowsDict.TryGetValue(args.WindowRef.AssetGUID, out var windows))
            {
                window = windows.FirstOrDefault(x => x.CurrentItemVM.Id == args.ItemId);
            }

            return window != null;
        }

        private void WindowOnClosedCallback(WindowContainer window)
        {
            var assetGUID = window.GetComponent<PrefabPoolInstance>().AssetGUID;

            inventoryVMF.ReturnItem(window);

            windowsDict[assetGUID].Remove(window);
        }

        private void CloseAllWindows()
        {
            foreach (var windows in windowsDict.Values)
            {
                foreach (var window in windows)
                {
                    CloseWindow(window);
                }

                windows.ReleaseListPool();
            }

            windowsDict.Clear();
        }

        private void CloseWindow(WindowContainer window)
        {
            window.CloseWindow();

            inventoryVMF.ReturnItem(window);
        }
    }
}