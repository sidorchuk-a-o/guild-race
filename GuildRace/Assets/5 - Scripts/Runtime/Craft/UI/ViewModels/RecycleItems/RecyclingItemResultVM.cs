using AD.Router;

namespace AD.Game.Craft
{
    public class RecyclingItemResultVM : RecyclingResultVM
    {
        public RecyclingItemsVM ReagentsVM { get; }

        public RecyclingItemResultVM(RecyclingItemResult data, CraftVMFactory craftVMF)
        {
            ReagentsVM = new(data.Reagents, craftVMF);
        }

        protected override void InitSubscribes()
        {
            ReagentsVM.AddTo(this);
        }
    }
}