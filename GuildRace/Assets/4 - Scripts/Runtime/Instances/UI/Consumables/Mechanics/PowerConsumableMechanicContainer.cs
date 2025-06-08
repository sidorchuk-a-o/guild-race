using System.Threading;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;

namespace Game.Instances
{
    public class PowerConsumableMechanicContainer : ConsumableMechanicContainer
    {
        private PowerConsumableVM mechanicVM;

        public override async UniTask Init(ConsumablesItemVM consumableVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            mechanicVM = consumableVM.MechanicVM as PowerConsumableVM;

            await base.Init(consumableVM, disp, ct);
        }

        protected override UITextData GetDesc()
        {
            return new(consumableVM.DescKey, mechanicVM.Power);
        }
    }
}