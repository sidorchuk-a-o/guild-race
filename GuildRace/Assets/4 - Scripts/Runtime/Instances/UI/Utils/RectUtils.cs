using UnityEngine;

namespace Game.Instances
{
    public static class RectUtils
    {
        public static Vector2 GetMapRectPosition(InstanceMapUIComponent map, Vector3 worldPosition)
        {
            return worldPosition * GetMapRatio(map);
        }

        public static Vector3 GetMapWorldPosition(InstanceMapUIComponent map, Vector2 rectPosition)
        {
            return rectPosition / GetMapRatio(map);
        }

        private static float GetMapRatio(InstanceMapUIComponent map)
        {
            var mapRect = map.transform as RectTransform;

            var mapImageWidth = map.MapImage.mainTexture.width;
            var pixelsPerUnit = map.MapImage.sprite.pixelsPerUnit;

            var mapRatio = mapRect.sizeDelta.x / mapImageWidth * pixelsPerUnit;

            return mapRatio;
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
    }
}