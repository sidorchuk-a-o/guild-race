using AD.Services.Router;
using Cysharp.Threading.Tasks;

namespace Game.Craft
{
    public class RecyclingReagentResultVM : RecyclingResultVM
    {
        public CurrencyVM CurrencyVM { get; set; }
        public int Amount { get; set; }

        public RecyclingReagentResultVM(RecyclingReagentResult data, CraftVMFactory craftVMF)
        {
            CurrencyVM = craftVMF.GetCurrency(data.Currency);
            Amount = data.Amount;
        }

        protected override void InitSubscribes()
        {
            CurrencyVM.AddTo(this);
        }
    }
}