using System.Collections.Generic;

namespace Game.Craft
{
    public class RecyclingItemResult : RecyclingResult
    {
        public IReadOnlyCollection<RecyclingItemInfo> Reagents { get; set; }
    }
}