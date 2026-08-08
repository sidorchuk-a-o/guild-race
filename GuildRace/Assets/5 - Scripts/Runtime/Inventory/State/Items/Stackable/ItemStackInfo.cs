using AD.ToolsCollection;
using R3;
using UnityEngine;

namespace AD.Game.Inventory
{
    public class ItemStackInfo
    {
        private readonly ItemStack data;

        private readonly ReactiveProperty<int> value = new(ItemStack.Default.Size);
        private readonly ReactiveEvent<StackChanged> onChanged = new();

        public int Size => data.Size;
        public int Value => value.Value;

        public bool IsFulled => Value == Size;
        public int AvailableSpace => Size - Value;

        public ItemStackInfo(in ItemStack data)
        {
            this.data = data;
        }

        public void AddValue(int value)
        {
            SetValue(this.value.Value + value);
        }

        public void SetValue(int value)
        {
            var prevValue = this.value.Value;
            var newValue = Mathf.Clamp(value, ItemStack.Default.Size, Size);

            this.value.Value = newValue;

            onChanged.OnNext(new(prevValue, newValue));
        }

        public ReadOnlyReactiveEvent<StackChanged> ObserveChanged()
        {
            return onChanged;
        }
    }
}