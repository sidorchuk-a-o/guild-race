using System.Collections.Generic;

namespace Game.Guild
{
    public class EmblemEM
    {
        public int Symbol { get; set; }
        public int SymbolColor { get; set; }
        public int Background { get; set; }
        public List<int> BackgroundColors { get; set; }

        public EmblemEM()
        {
        }

        public EmblemEM(EmblemInfo info)
        {
            Symbol = info.Symbol;
            SymbolColor = info.SymbolColor;
            Background = info.Background;
            BackgroundColors = new(info.BackgroundColors);
        }
    }
}