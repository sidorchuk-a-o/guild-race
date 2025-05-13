using AD.Services.Router;
using Cysharp.Threading.Tasks;

namespace Game.Craft
{
    public class RecyclingItemResultVM : RecyclingResultVM
    {
        public RecyclingItemsVM ReagentsVM { get; }

        public RecyclingItemResultVM(RecyclingItemResult data, CraftVMFactory craftVMF)
        {
            ReagentsVM = new RecyclingItemsVM(data.Reagents, craftVMF);
        }

        protected override void InitSubscribes()
        {
            ReagentsVM.AddTo(this);
        }
    }
}