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

        public override async UniTask Init(ConsumablesDataVM dataVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(dataVM, disp, ct);

            if (ct.IsCancellationRequested) return;

            var mechanicVM = dataVM.MechanicVM as ThreatConsumableVM;

            await threatItem.Init(mechanicVM.ThreatVM, disp, ct);
        }
    }
}