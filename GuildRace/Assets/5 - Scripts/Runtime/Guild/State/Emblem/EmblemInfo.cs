// ReSharper disable ConvertToAutoPropertyWithPrivateSetter

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace AD.Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class EmblemInfo
    {
        private int symbol;
        private int symbolColor;
        private int background;
        private List<int> backgroundColors = new();

        public int Symbol => symbol;
        public int SymbolColor => symbolColor;
        public int Background => background;
        public IReadOnlyList<int> BackgroundColors => backgroundColors;

        public void SetSymbol(int index)
        {
            symbol = index;
        }

        public void SetSymbolColor(int index)
        {
            symbolColor = index;
        }

        public void SetBackground(int index)
        {
            background = index;
        }

        public void SetBackgroundColors(IEnumerable<int> indexes)
        {
            backgroundColors = indexes.ToList();
        }
    }
}