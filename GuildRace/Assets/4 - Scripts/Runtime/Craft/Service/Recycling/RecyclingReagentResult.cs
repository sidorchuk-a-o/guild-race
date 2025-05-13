using AD.Services.Store;

namespace Game.Craft
{
    public class RecyclingReagentResult : RecyclingResult
    {
        public CurrencyKey Currency { get; set; }
        public int Amount { get; set; }
    }
}