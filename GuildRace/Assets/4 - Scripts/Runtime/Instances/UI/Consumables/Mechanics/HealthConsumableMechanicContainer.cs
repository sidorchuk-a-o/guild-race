using System.Threading;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;

namespace Game.Instances
{
    public class HealthConsumableMechanicContainer : ConsumableMechanicContainer
    {
        private HealthConsumableVM mechanicVM;

        public override async UniTask Init(ConsumablesDataVM dataVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            mechanicVM = dataVM.MechanicVM as HealthConsumableVM;

            await base.Init(dataVM, disp, ct);
        }

        protected override UITextData GetDesc()
        {
            return new(dataVM.DescKey, mechanicVM.Health);
        }
    }
}