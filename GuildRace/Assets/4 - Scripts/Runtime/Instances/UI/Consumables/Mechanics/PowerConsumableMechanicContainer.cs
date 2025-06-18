using System.Threading;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;

namespace Game.Instances
{
    public class PowerConsumableMechanicContainer : ConsumableMechanicContainer
    {
        private PowerConsumableVM mechanicVM;

        public override async UniTask Init(ConsumablesDataVM dataVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            mechanicVM = dataVM.MechanicVM as PowerConsumableVM;

            await base.Init(dataVM, disp, ct);
        }

        protected override UITextData GetDesc()
        {
            return new(dataVM.DescKey, mechanicVM.Power);
        }
    }
}