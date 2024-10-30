using System;
using UniRx;
using UnityEngine;

namespace Game.Inventory
{
    public class ItemBoundsInfo : IObservable<BoundsInt>
    {
        private readonly ReactiveProperty<BoundsInt> bounds = new();

        public BoundsInt Value { get; private set; }

        public Vector3Int Position { get; private set; }
        public Vector3Int Size { get; private set; }

        public Vector3Int DefaultSize { get; }
        public Vector3Int SizeWithoutRotation { get; private set; }

        public bool IsRotated { get; private set; }

        public ItemBoundsInfo(in ItemSize size)
        {
            Position = Vector3Int.zero;
            DefaultSize = new(size.Width, size.Height);

            SetSize(DefaultSize);
        }

        public void SetPosition(in Vector3Int position)
        {
            Position = position;

            UpdateBounds();
        }

        public void Rotate()
        {
            if (Size.x == Size.y)
            {
                return;
            }

            IsRotated = !IsRotated;

            UpdateSize();
        }

        public void AddSize(in Vector3Int value)
        {
            SetSize(SizeWithoutRotation + value);
        }

        public void SetSize(in Vector3Int size, bool isRotated)
        {
            IsRotated = isRotated;

            SetSize(size);
        }

        public void SetSize(in Vector3Int size)
        {
            SizeWithoutRotation = size;

            UpdateSize();
        }

        private void UpdateSize()
        {
            if (Size.x == Size.y && IsRotated)
            {
                IsRotated = false;
            }

            Size = new Vector3Int
            {
                x = IsRotated ? SizeWithoutRotation.y : SizeWithoutRotation.x,
                y = IsRotated ? SizeWithoutRotation.x : SizeWithoutRotation.y,
            };

            UpdateBounds();
        }

        private void UpdateBounds()
        {
            Value = new(Position, Size);
            bounds.Value = Value;
        }

        IDisposable IObservable<BoundsInt>.Subscribe(IObserver<BoundsInt> observer)
        {
            return bounds.Subscribe(observer);
        }
    }
}