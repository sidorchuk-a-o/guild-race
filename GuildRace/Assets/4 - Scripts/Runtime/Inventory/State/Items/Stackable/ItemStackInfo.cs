using System;
using UniRx;
using UnityEngine;

namespace Game.Inventory
{
    public class ItemStackInfo
    {
        private readonly ItemStack data;
        private readonly ReactiveProperty<int> value = new(ItemStack.Default.Size);

        public int Size => data.Size;
        public int Value => value.Value;

        public bool IsFulled => Value == Size;
        public int AvailableSpace => Size - Value;

        public ItemStackInfo(in ItemStack data)
        {
            this.data = data;
        }

        public void SetValue(int value)
        {
            this.value.Value = Mathf.Clamp(value, ItemStack.Default.Size, Size);
        }

        public void AddValue(int value)
        {
            SetValue(this.value.Value + value);
        }

        public IObservable<int> ObserveValue()
        {
            return value;
        }
    }
}