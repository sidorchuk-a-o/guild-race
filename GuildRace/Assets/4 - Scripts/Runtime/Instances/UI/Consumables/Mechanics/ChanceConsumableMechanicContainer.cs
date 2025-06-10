using System.Threading;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Instances
{
    public class ChanceConsumableMechanicContainer : ConsumableMechanicContainer
    {
        [Header("Chance Params")]
        [SerializeField] private UIText chanceText;

        public override async UniTask Init(ConsumablesDataVM dataVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(dataVM, disp, ct);

            if (ct.IsCancellationRequested) return;

            var mechanicVM = dataVM.MechanicVM as ChanceConsumableVM;

            chanceText.SetTextParams(new(chanceText.LocalizeKey, mechanicVM.Chance));
        }
    }
}