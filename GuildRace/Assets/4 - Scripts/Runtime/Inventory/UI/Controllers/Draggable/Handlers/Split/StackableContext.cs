using Cysharp.Threading.Tasks;

namespace Game.Inventory
{
    public class StackableContext
    {
        public ItemVM SelectedItem { get; set; }
        public ItemVM HoveredItem { get; set; }
        public ItemsGridVM SelectedGrid { get; set; }

        public bool IsRotated { get; set; }
        public PositionOnGrid PositionOnGrid { get; set; }

        public UniTaskCompletionSource CompleteTask { get; set; }
    }
}