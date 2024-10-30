using Cysharp.Threading.Tasks;

namespace Game.Inventory
{
    public class StackableContext
    {
        public ItemVM SelectedItemVM { get; set; }
        public ItemVM HoveredItemVM { get; set; }
        public ItemsGridVM SelectedGridVM { get; set; }

        public bool IsRotated { get; set; }
        public PositionOnGrid PositionOnGrid { get; set; }

        public UniTaskCompletionSource CompleteTask { get; set; }
    }
}