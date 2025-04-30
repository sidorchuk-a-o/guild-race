using System.Collections.Generic;
using System.Linq;

namespace Game.Guild
{
    public class EmblemInfo
    {
        public int Symbol { get; private set; }
        public int SymbolColor { get; private set; }
        public int Background { get; private set; }
        public IReadOnlyList<int> BackgroundColors { get; private set; }

        public EmblemInfo()
        {
            BackgroundColors = new List<int>();
        }

        public void SetSymbol(int index)
        {
            Symbol = index;
        }

        public void SetSymbolColor(int index)
        {
            SymbolColor = index;
        }

        public void SetBackground(int index)
        {
            Background = index;
        }

        public void SetBackgroundColors(IEnumerable<int> indexes)
        {
            BackgroundColors = indexes.ToList();
        }
    }
}