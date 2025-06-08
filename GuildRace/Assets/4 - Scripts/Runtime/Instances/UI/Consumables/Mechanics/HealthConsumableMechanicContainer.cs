using System.Threading;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;

namespace Game.Instances
{
    public class HealthConsumableMechanicContainer : ConsumableMechanicContainer
    {
        private HealthConsumableVM mechanicVM;

        public override async UniTask Init(ConsumablesItemVM consumableVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            mechanicVM = consumableVM.MechanicVM as HealthConsumableVM;

            await base.Init(consumableVM, disp, ct);
        }

        protected override UITextData GetDesc()
        {
            return new(consumableVM.DescKey, mechanicVM.Health);
        }
    }
}