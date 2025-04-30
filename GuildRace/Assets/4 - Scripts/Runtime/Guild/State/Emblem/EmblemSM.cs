using Newtonsoft.Json;
using System.Collections.Generic;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class EmblemSM
    {
        [ES3Serializable] private int symbol;
        [ES3Serializable] private int symbolColor;
        [ES3Serializable] private int background;
        [ES3Serializable] private List<int> backgroundColors;

        public EmblemSM(EmblemInfo info)
        {
            symbol = info.Symbol;
            symbolColor = info.SymbolColor;
            background = info.Background;
            backgroundColors = new(info.BackgroundColors);
        }

        public EmblemInfo GetValue()
        {
            var info = new EmblemInfo();

            info.SetSymbol(symbol);
            info.SetSymbolColor(symbolColor);
            info.SetBackground(background);
            info.SetBackgroundColors(backgroundColors);

            return info;
        }
    }
}