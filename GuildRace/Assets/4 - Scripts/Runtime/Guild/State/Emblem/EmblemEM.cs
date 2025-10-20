using System;
using System.Collections.Generic;

namespace Game.Guild
{
    public class EmblemEM : IEquatable<EmblemEM>
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

        // == IEquatable ==

        public bool Equals(EmblemEM other)
        {
            return Symbol == other.Symbol
                && SymbolColor == other.SymbolColor
                && Background == other.Background
                && checkBackgroundColors(other.BackgroundColors);

            bool checkBackgroundColors(List<int> otherColors)
            {
                for (var i = 0; i < BackgroundColors.Count && i < otherColors.Count; i++)
                {
                    var c1 = BackgroundColors[i];
                    var c2 = otherColors[i];

                    if (c1 != c2)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        // == ToString ==

        public override string ToString()
        {
            return $"{Symbol}-{SymbolColor} | {Background}-({string.Join("-", BackgroundColors)})";
        }
    }
}