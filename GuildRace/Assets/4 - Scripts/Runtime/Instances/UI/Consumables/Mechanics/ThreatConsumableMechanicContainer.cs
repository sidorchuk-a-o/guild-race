using System.Threading;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Instances
{
    public class ThreatConsumableMechanicContainer : ConsumableMechanicContainer
    {
        [Header("Threat Params")]
        [SerializeField] private ThreatDataItem threatItem;

        private ThreatConsumableVM mechanicVM;

        public override async UniTask Init(ConsumablesItemVM consumableVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            mechanicVM = consumableVM.MechanicVM as ThreatConsumableVM;

            await base.Init(consumableVM, disp, ct);

            if (ct.IsCancellationRequested) return;

            await threatItem.Init(mechanicVM.ThreatVM, disp, ct);
        }
    }
}