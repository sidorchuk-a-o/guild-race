using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game.Inventory
{
    public static class BoundsExt
    {
        private static readonly Vector3Int intersectsOffset = new(1, 1);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects2D(this in BoundsInt a, in BoundsInt b)
        {
            var aMin = a.min;
            var aMax = a.max - intersectsOffset;

            var bMin = b.min;
            var bMax = b.max - intersectsOffset;

            return aMin.x <= bMax.x && aMax.x >= bMin.x
                && aMin.y <= bMax.y && aMax.y >= bMin.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains2D(this in BoundsInt bounds, in Vector3Int position)
        {
            return position.x >= bounds.xMin && position.x < bounds.xMax
                && position.y >= bounds.yMin && position.y < bounds.yMax;
        }
    }
}