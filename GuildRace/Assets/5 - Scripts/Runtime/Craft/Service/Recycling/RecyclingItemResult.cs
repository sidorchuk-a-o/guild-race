using System.Collections.Generic;

namespace AD.Game.Craft
{
    public class RecyclingItemResult : RecyclingResult
    {
        public IReadOnlyCollection<RecyclingItemInfo> Reagents { get; set; }
    }
}