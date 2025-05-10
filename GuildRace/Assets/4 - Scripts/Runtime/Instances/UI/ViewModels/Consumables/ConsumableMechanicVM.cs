using AD.Services.Router;

namespace Game.Instances
{
    public class ConsumableMechanicVM : ViewModel
    {
        private readonly ConsumableMechanicHandler handler;

        public ConsumableMechanicVM(ConsumableMechanicHandler handler)
        {
            this.handler = handler;
        }

        protected override void InitSubscribes()
        {
        }
    }
}