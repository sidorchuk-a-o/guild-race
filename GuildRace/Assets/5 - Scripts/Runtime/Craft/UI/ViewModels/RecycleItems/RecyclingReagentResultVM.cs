using AD.Router;
using AD.Store;

namespace AD.Game.Craft
{
    public class RecyclingReagentResultVM : RecyclingResultVM
    {
        public CurrencyVM CurrencyVM { get; set; }

        public RecyclingReagentResultVM(RecyclingReagentResult data, CraftVMFactory craftVMF)
        {
            CurrencyVM = craftVMF.StoreVMF.GetCurrency(data.Amount);
        }

        protected override void InitSubscribes()
        {
            CurrencyVM.AddTo(this);
        }
    }
}