using UnityEngine;

namespace Game.Inventory
{
    public static class RectUtils
    {
        private static UIParams parameters;

        public static void Init(UIParams parameters)
        {
            RectUtils.parameters = parameters;
        }

        public static PositionOnGrid GetPositionOnGrid(
            in Vector2 cursorPosition,
            RectTransform gridTransform,
            ItemVM selectedItem)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rect: gridTransform,
                screenPoint: cursorPosition,
                cam: null,
                localPoint: out var positionInGrid);

            var cursorOnGrid = GetCellPosition(positionInGrid);

            if (selectedItem == null)
            {
                return new()
                {
                    Item = cursorOnGrid,
                    Cursor = cursorOnGrid
                };
            }
            else
            {
                var itemSize = selectedItem.BoundsVM.Size;

                positionInGrid.x -= (itemSize.x - 1f) * parameters.CellSize / 2f;
                positionInGrid.y += (itemSize.y - 1f) * parameters.CellSize / 2f;

                var itemOnGrid = GetCellPosition(positionInGrid);

                return new()
                {
                    Item = itemOnGrid,
                    Cursor = cursorOnGrid
                };
            }
        }

        private static Vector3Int GetCellPosition(in Vector2 positionInGrid)
        {
            return new()
            {
                x = Mathf.FloorToInt(positionInGrid.x / parameters.CellSize),
                y = Mathf.FloorToInt(-positionInGrid.y / parameters.CellSize)
            };
        }

        public static Vector2 GetLocalPosition(this RectTransform rect, Vector2 position)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rect: rect,
                screenPoint: position,
                cam: null,
                localPoint: out var localPosition);

            return localPosition;
        }

        public static void ApplyItemBounds(this Transform transform, in BoundsInt itemBounds)
        {
            ApplyItemBounds(transform as RectTransform, itemBounds);
        }

        public static void ApplyItemBounds(this RectTransform rect, in BoundsInt itemBounds)
        {
            if (rect == null)
            {
                return;
            }

            rect.sizeDelta = new Vector2
            {
                x = itemBounds.size.x * parameters.CellSize,
                y = itemBounds.size.y * parameters.CellSize
            };

            rect.anchoredPosition = new Vector2
            {
                x = itemBounds.x * parameters.CellSize,
                y = -itemBounds.y * parameters.CellSize
            };

            rect.ForceUpdateRectTransforms();
        }

        public static void ApplyItemSize(this Transform transform, in Vector3Int itemSize)
        {
            ApplyItemSize(transform as RectTransform, itemSize);
        }

        public static void ApplyItemSize(this RectTransform rect, in Vector3Int itemSize)
        {
            if (rect == null)
            {
                return;
            }

            rect.sizeDelta = new Vector2
            {
                x = itemSize.x * parameters.CellSize,
                y = itemSize.y * parameters.CellSize
            };

            rect.ForceUpdateRectTransforms();
        }

        public static void ClampPositionToParent(this RectTransform rect)
        {
            var canvas = rect.GetComponentInParent<Canvas>();

            if (canvas == null)
            {
                return;
            }

            var canvasRect = canvas.transform as RectTransform;

            var rectCorners = new Vector3[4];
            var canvasCorners = new Vector3[4];

            rect.ForceUpdateRectTransforms();
            rect.GetWorldCorners(rectCorners);
            canvasRect.GetWorldCorners(canvasCorners);

            var height = rect.rect.size.y;
            var width = rect.rect.size.x;

            if (rectCorners[0].y <= canvasCorners[0].y)
            {
                rect.SetInsetAndSizeFromParentEdgeWithCurrentAnchors(RectTransform.Edge.Bottom, 0, height);
            }
            else if (rectCorners[1].y >= canvasCorners[1].y)
            {
                rect.SetInsetAndSizeFromParentEdgeWithCurrentAnchors(RectTransform.Edge.Top, 0, height);
            }

            if (rectCorners[0].x <= canvasCorners[0].x)
            {
                rect.SetInsetAndSizeFromParentEdgeWithCurrentAnchors(RectTransform.Edge.Left, 0, width);
            }
            else if (rectCorners[2].x >= canvasCorners[2].x)
            {
                rect.SetInsetAndSizeFromParentEdgeWithCurrentAnchors(RectTransform.Edge.Right, 0, width);
            }
        }

        public static void SetInsetAndSizeFromParentEdgeWithCurrentAnchors(
            this RectTransform child,
            RectTransform.Edge fixedEdge,
            float newInset,
            float newSize)
        {
            var axisIndex = (int)fixedEdge / 2;
            var sizeChange = newSize - child.rect.size[axisIndex];

            var origAnchorMin = child.anchorMin;
            var origAnchorMax = child.anchorMax;
            var origSizeDelta = child.sizeDelta;

            child.SetInsetAndSizeFromParentEdge(fixedEdge, newInset, newSize);
            child.RestoreAnchorsIfNeeded(axisIndex, origSizeDelta, ref origAnchorMin, ref origAnchorMax, sizeChange);
        }

        private static void RestoreAnchorsIfNeeded(
            this RectTransform child,
            int axisIndex,
            Vector2 origSizeDelta,
            ref Vector2 origAnchorMin,
            ref Vector2 origAnchorMax,
            float sizeChange)
        {
            var localPos = child.localPosition;
            var restoreNeeded = false;

            if (child.anchorMin != origAnchorMin)
            {
                child.anchorMin = origAnchorMin;
                restoreNeeded = true;
            }

            if (child.anchorMax != origAnchorMax)
            {
                child.anchorMax = origAnchorMax;
                restoreNeeded = true;
            }

            if (restoreNeeded)
            {
                child.localPosition = localPos;
                origSizeDelta[axisIndex] += sizeChange;
                child.sizeDelta = origSizeDelta;
            }
        }
    }
}