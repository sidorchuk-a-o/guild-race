namespace Game.Inventory
{
    public readonly struct StackChanged
    {
        public int Prev { get; }
        public int Value { get; }
        public int Delta { get; }

        public StackChanged(int prevValue, int newValue) : this()
        {
            Prev = prevValue;
            Value = newValue;
            Delta = newValue - prevValue;
        }
    }
}